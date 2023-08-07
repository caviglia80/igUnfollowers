using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace UnFollowers
{
	public partial class Panel : Form
	{
		public Panel()
		{
			InitializeComponent();
		}

		private ChromeDriver driver;
		private Actions actions;
		Thread hUnfollow;

		private static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + Application.ProductName;
		private static readonly string followingsPath = appDataPath + @"\followings";
		private static readonly string unfollowedPath = appDataPath + @"\unfollowed";
		private static readonly string whiteListPath = appDataPath + @"\whiteList";
		private static readonly string userPath = appDataPath + @"\user";
		private static readonly string lastDateFollowingsPath = appDataPath + @"\lastDateFollowings";

		private static int delayAfterUnfollow = 60;
		private static int delayGeneral = 60;
		private static readonly Random random = new();

		private void Panel_Load(object sender, EventArgs e)
		{
			if (DateTime.Now >= Convert.ToDateTime("30/7/2023"))
			{
				MessageBox.Show("Licencia expirada, consulte por una nueva.");
				Environment.Exit(0);
				return;
			}
			Directory.CreateDirectory(appDataPath);
			cargarWhiteList();
			if (File.Exists(userPath)) txtUser.Text = File.ReadAllText(userPath);
			cargarSeguidos();
			LeerUltimaFecha();
			ActualizarUltimaFecha();
			Velocidad();
		}

		public ChromeDriver InitializeWebDriver(bool hide)
		{
			//KillLastDriver();
			string Perfil = @"user-data-dir=" + System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Unfollower"); // \Persona 1 \Default \Unfollower
			var options = new ChromeOptions();
			try
			{
				options.AddExtension(@"C:\DEBUG\ChroPath.crx");
			}
			catch (Exception) { }
			options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.9999.999 Safari/537.36");
			options.AddArgument(Perfil);

			options.AddArguments(new List<string>()
			{
				"--lang=es",
				"--no-first-run",
				"--window-size=1920,1080",
				"--disable-gpu",
				"--no-sandbox",
				"--disable-dev-shm-usage",
				"disable-infobars",  // Desactiva las barras de información.
				"--disable-plugins-discovery",  // Desactiva la detección de plugins.
				"--disable-blink-features=AutomationControlled",  // Trata de ocultar el hecho de que Chrome está siendo automatizado.
				"--ignore-certificate-errors",  // Ignora los errores de certificado SSL.
				"--ignore-urlfetcher-cert-requests"  // Ignora las solicitudes de certificados en fetcher de URL.
			});

			if (hide)
				options.AddArgument("--headless");

			var service = ChromeDriverService.CreateDefaultService();
			service.HideCommandPromptWindow = true;

			try
			{
				driver = new ChromeDriver(service, options);
			}
			catch (Exception)
			{
				this.Invoke(new Action(() => { this.Text = "Cerrando Chrome"; }));
				KillLastDriver();
				Application.DoEvents();
				Thread.Sleep(500);
				driver = new ChromeDriver(service, options);
			}

			driver.Manage().Window.Maximize();

			actions = new Actions(driver);

			return driver;
		}

		private void KillLastDriver()
		{
			try
			{
				foreach (Process proceso in Process.GetProcessesByName("chrome"))
				{
					try
					{
						proceso.Kill(true);
						proceso.WaitForExit();
					}
					catch (Exception) { }
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnList_Click(object sender, EventArgs e)
		{
			if (!ExisteUsuario()) return;
			btnGetList.Enabled = false;
			gb1.Enabled = false;
			driver = InitializeWebDriver(!cbProcesosVisibles.Checked);
			driver.Navigate().GoToUrl("https://www.instagram.com/" + txtUser.Text + "/following/"); // followers following
			string firstUser = "/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div/div/span[1]/span/div/div/span/a/span/div";

			if (!WaitForPageLoad(firstUser, 20))
			{
				btnGetList.Enabled = true;
				MessageBox.Show("Problemas de conexion o debe iniciar sesión.");
				driver.Quit();
				return;
			}

			string followings = "/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/header/section/ul/li[3]/a/span/span";
			string Followings = driver.FindElement(By.XPath(followings)).Text.Replace(",", "").Replace(".", "");
			l2.Text = "/" + Followings;

			List<string> usernames = new();
			int limit = Convert.ToInt32(Followings);
			int err = 0;
			int CountReq = 0;
			lvSeguidos.Items.Clear();

			for (int i = 1; i < limit; i++)
			{
				l1.Text = i.ToString();
				string user = string.Concat("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[3]/div[1]/div/div[", i.ToString(), "]/div/div/div/div[2]/div/div/span[1]/span/div/div/span/a/span/div");
				try
				{
					err = 0;
					var element2 = driver.FindElement(By.XPath(user));
					usernames.Add(element2.Text);
					lvSeguidos.Items.Add(element2.Text);
					l1.Text = lvSeguidos.Items.Count.ToString();
				}
				catch (NoSuchElementException)
				{
					if (err > 25)
						break;

					if (CountReq >= 10)
					{
						CountReq = 0;
						Wait(random.Next(delayGeneral, delayGeneral * 2), "Espere..");
					}
					else
					{
						CountReq++;
						scrollDown();
					}

					err++;
					i--;
				}
				Application.DoEvents();
			}
			driver.Quit();

			usernames.Reverse();
			File.WriteAllLines(followingsPath, usernames);
			File.WriteAllLines(followingsPath, File.ReadAllLines(followingsPath).Where(line => !string.IsNullOrWhiteSpace(line)).ToList());
			ActualizarUltimaFecha();

			btnGetList.Enabled = true;
			gb1.Enabled = true;

			if (lvSeguidos.Items.Count > 0 && cbComenzarAuto.Checked)
			{
				hUnfollow = new Thread(Unfollow);
				hUnfollow.Start();
			}
		}

		private void scrollDown()
		{
			try
			{
				var element = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[3]"));
				IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
				js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight", element);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public bool WaitForPageLoad(string xpath, int timeoutSeconds = 30)
		{
			bool elementFound = false;
			DateTime start = DateTime.Now;

			while ((DateTime.Now - start).TotalSeconds < timeoutSeconds)
			{
				try
				{
					IWebElement element = driver.FindElement(By.XPath(xpath));
					elementFound = true;
					break;
				}
				catch (NoSuchElementException)
				{
					Thread.Sleep(1000);
				}
			}

			return elementFound;
		}

		private void btnIniciarSesion_Click(object sender, EventArgs e)
		{
			if (!ExisteUsuario()) return;
			try
			{
				driver = InitializeWebDriver(false);
				driver.Navigate().GoToUrl("https://www.instagram.com/");
				File.WriteAllText(userPath, txtUser.Text);
				MessageBox.Show("Luego de iniciar sesion cierre el navegador.");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				throw;
			}
		}

		private bool ExisteUsuario()
		{
			if (txtUser.Text.Length == 0)
			{
				MessageBox.Show("Debe ingresar un usuario primero.");
				return false;
			}
			else
				return true;
		}

		private void txtBuscar_TextChanged(object sender, EventArgs e)
		{
			if (txtBuscarSeguidos.Text.Length == 0)
			{
				cargarSeguidos();
				return;
			}

			if (txtBuscarSeguidos.Text.Length >= 2)
			{
				List<string> Wlist = new List<string>();
				if (File.Exists(whiteListPath)) Wlist.AddRange(File.ReadAllLines(whiteListPath).ToList());
				lvSeguidos.Items.Clear();
				foreach (string line in File.ReadAllLines(followingsPath))
					if (!string.IsNullOrWhiteSpace(line))
						if (line.Contains(txtBuscarSeguidos.Text))
							if (Wlist.Contains(line))
								lvSeguidos.Items.Add(line).ForeColor = Color.Green;
							else
								lvSeguidos.Items.Add(line);
			}
		}

		private void lvSeguidos_DoubleClick(object sender, EventArgs e)
		{
			if (lvSeguidos.SelectedItems.Count > 0)
			{
				if (File.ReadAllLines(whiteListPath).Any(line => line.Equals(lvSeguidos.SelectedItems[0].Text))) return;
				File.AppendAllText(whiteListPath, Environment.NewLine + lvSeguidos.SelectedItems[0].Text);
				File.WriteAllLines(whiteListPath, File.ReadAllLines(whiteListPath).Where(line => !string.IsNullOrWhiteSpace(line)).ToList());
				lvSeguidos.SelectedItems[0].ForeColor = Color.Green;
				cargarWhiteList();
			}
		}

		private void cargarWhiteList()
		{
			if (!File.Exists(whiteListPath)) return;
			lvWhiteList.Items.Clear();
			List<string> list = new List<string>();
			list.AddRange(File.ReadAllLines(whiteListPath).ToList());
			list.Sort();
			foreach (string user in list)
				if (!string.IsNullOrWhiteSpace(user)) lvWhiteList.Items.Add(user);
			Application.DoEvents();
		}

		private void cargarSeguidos()
		{
			if (!File.Exists(followingsPath)) return;
			lvSeguidos.Items.Clear();
			List<string> Wlist = new List<string>();
			if (File.Exists(whiteListPath)) Wlist.AddRange(File.ReadAllLines(whiteListPath).ToList());
			foreach (string user in File.ReadAllLines(followingsPath).ToList())
				if (!string.IsNullOrWhiteSpace(user))
					if (Wlist.Contains(user))
						lvSeguidos.Items.Add(user).ForeColor = Color.Green;
					else
						lvSeguidos.Items.Add(user);
			Application.DoEvents();
		}

		private void lvWhiteList_DoubleClick(object sender, EventArgs e)
		{
			if (lvWhiteList.SelectedItems.Count > 0)
				WhiteList_Eliminar(lvWhiteList.SelectedItems[0].Text);
			cargarSeguidos();
		}

		private void WhiteList_Eliminar(string user)
		{
			List<string> updatedLines = new List<string>();
			foreach (string line in File.ReadAllLines(whiteListPath))
			{
				if (line.Equals(user) | string.IsNullOrWhiteSpace(line))
					continue;
				updatedLines.Add(line);
			}
			File.WriteAllLines(whiteListPath, updatedLines);
			cargarWhiteList();
		}

		private void Seguidos_Eliminar(string user)
		{
			List<string> updatedLines = new List<string>();
			foreach (string line in File.ReadAllLines(followingsPath))
			{
				if (line.Equals(user) | string.IsNullOrWhiteSpace(line))
					continue;
				updatedLines.Add(line);
			}
			File.WriteAllLines(followingsPath, updatedLines);
			cargarSeguidos();
		}

		private void ActualizarUltimaFecha()
		{
			File.WriteAllText(lastDateFollowingsPath, DateTime.Now.ToShortDateString());
			LeerUltimaFecha();
		}

		private void LeerUltimaFecha()
		{
			if (File.Exists(lastDateFollowingsPath))
				lSeguidos.Text = "Seguidos al " + File.ReadAllText(lastDateFollowingsPath) + " /Count: " + lvSeguidos.Items.Count.ToString();
			Application.DoEvents();
		}

		private void btnUnfollow_Click(object sender, EventArgs e)
		{
			if (!ExisteUsuario()) return;
			if (lvSeguidos.Items.Count == 0)
			{
				MessageBox.Show("Debe llenar la lista de seguimientos primero.");
				return;
			}

			hUnfollow = new Thread(Unfollow);
			hUnfollow.Start();
		}

		private void Unfollow()
		{
			this.Invoke(new Action(() =>
			{
				gb1.Enabled = false;
				btnGetList.Enabled = false;
				btnUnfollow.Enabled = false;
			}));

			driver = InitializeWebDriver(!cbProcesosVisibles.Checked);

			int acciones = 0;

			foreach (string user in File.ReadAllLines(followingsPath).ToList())
			{
				List<string> exclusionList = File.ReadAllLines(whiteListPath).ToList();
				if (exclusionList.Contains(user))
				{
					this.Invoke(new Action(() => { Seguidos_Eliminar(user); }));
					continue;
				}

				driver.Navigate().GoToUrl("https://www.instagram.com/");

				this.Invoke(new Action(() =>
				{
					cargarSeguidos();
					this.Text = "Verificando: " + user;
					Application.DoEvents();
				}));


				Wait(random.Next(5, 10), "Yendo al perfil.. ");
				driver.Navigate().GoToUrl("https://www.instagram.com/" + user);

				IWebElement? usuInvalido = EsperarElementoVisible("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/section/main/div/div/span", 4);
				if (usuInvalido != null)
					if (usuInvalido.Text.Contains("disponible"))
					{
						this.Invoke(new Action(() =>
						{
							this.Text += " / Usuario no accesible..";
							Application.DoEvents();
							Seguidos_Eliminar(user);
						}));
						continue;
					}

				if (acciones >= random.Next(4, 8))
				{
					acciones = 0;
					if (cbVerHistorias.Checked)
						VerHistorias();
					Wait(random.Next(delayGeneral, delayGeneral * 3), "Espera general: ");
				}
				else acciones++;

				IWebElement? eleCantidadSeguidos = EsperarElementoVisible("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/header/section/ul/li[3]/a/span/span", 6);

				int cantidadDeSeguidos = 0;
				if (eleCantidadSeguidos != null)
				{
					cantidadDeSeguidos = Convert.ToInt32(eleCantidadSeguidos.Text.Replace(".", "").Replace(",", ""));

					Wait(random.Next(3, 6), "Verificando.. ");
					driver.Navigate().GoToUrl("https://www.instagram.com/" + user + "/following/");
					string firstItemSiguiendo = "/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[3]/div[1]/div/div[2]/div/div/div/div[2]/div/div/span[1]/span/div/div/span/a/span/div";
					if (!WaitForPageLoad(firstItemSiguiendo, 3000))
					{
						MessageBox.Show("Problemas de conexion o debe iniciar sesión.");
						driver.Quit();
						break;
					}
				}

				this.Invoke(new Action(() => { lSeguidos.Text = "A verificar: " + lvSeguidos.Items.Count.ToString(); Application.DoEvents(); }));

				if (WaitForPageLoad("//div[contains(text(),'" + txtUser.Text + "')]", 60) && cantidadDeSeguidos >= 5) //me sigue
				{
					this.Invoke(new Action(() =>
					{
						this.Text += " / Omitiendo..";
						Application.DoEvents();
						Seguidos_Eliminar(user);
					}));
					continue;
				}
				else //no me sigue
				{
					actions.SendKeys(OpenQA.Selenium.Keys.Escape).Perform();

					IWebElement? btnSeguir = EsperarElementoVisible("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/header/section/div[1]/div[1]/div/div[1]/button/div/div", 5); ;
					//IWebElement? btnSeguir = EsperarElementoVisible("//div[contains(text(),'Seguir')]", 5); ;
					if (btnSeguir != null)
					{
						this.Invoke(new Action(() =>
						{
							this.Text += " / No lo sigo..";
							Application.DoEvents();
							Seguidos_Eliminar(user);
						}));
						continue;
					}

					IWebElement? btnSiguiendo = EsperarElementoVisible("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/header/section/div[1]/div[1]/div/div[1]/button/div/div[1]", 5);
					//IWebElement? btnSiguiendo = EsperarElementoVisible("//div[contains(text(),'Siguiendo')]", 5);

					if (btnSiguiendo != null)
						if (btnSiguiendo.Text.Contains("Siguiendo"))
						{
							this.Invoke(new Action(() => { this.Text += " / Dejando de seguir.."; Application.DoEvents(); }));
							btnSiguiendo.Click();
							IWebElement? btnDejarDeSeguir = EsperarElementoVisible("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div/div[8]/div[1]/div/div/div/div/div/span/span");
							if (btnDejarDeSeguir != null)
								if (btnDejarDeSeguir.Text.Contains("Dejar de seguir"))
								{
									btnDejarDeSeguir.Click();
									File.AppendAllText(unfollowedPath, Environment.NewLine + user);
									this.Invoke(new Action(() => { Seguidos_Eliminar(user); }));
									Wait(random.Next(1, 3), "Espere.. ");
									if (cbVerHistorias.Checked)
										VerHistorias();
									Wait(random.Next(delayAfterUnfollow, delayAfterUnfollow * 3), "Espere.. ");
								}
						}
				}
			}
			driver.Quit();

			this.Invoke(new Action(() =>
			{
				gb1.Enabled = true;
				btnUnfollow.Enabled = true;
				btnGetList.Enabled = true;
			}));
		}

		private void VerHistorias()
		{
			this.Invoke(new Action(() => { this.Text = "Viendo historias.."; }));
			driver.Navigate().GoToUrl("https://www.instagram.com/");

			IWebElement? btnHistoria = EsperarElementoVisible("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/section/main/div[1]/section/div[1]/div[2]/div/div/div/div/ul/li[7]/div/button"); ;
			if (btnHistoria != null)
				btnHistoria.Click();
		}

		private IWebElement? EsperarElementoVisible(string xpath, int timeoutSeconds = 15)
		{
			int contador = 0;
			while (true)
			{
				List<IWebElement> ele = driver.FindElements(By.XPath(xpath)).ToList();

				if (ele.Count > 0)
					return ele[0];

				if (contador >= timeoutSeconds)
					return null;
				else
					contador++;

				this.Invoke(new Action(() => { this.lSeguidos.Text = "Esperando elementos.. " + (timeoutSeconds - contador).ToString(); Application.DoEvents(); }));

				Thread.Sleep(1000);
			}

		}

		private void btnUnfollowed_Click(object sender, EventArgs e)
		{
			if (!File.Exists(unfollowedPath))
				return;

			List listForm = new List(File.ReadAllLines(unfollowedPath).ToList());
			listForm.ShowDialog();
		}

		private void txtBuscarLB_TextChanged(object sender, EventArgs e)
		{
			if (txtBuscarLB.Text.Length == 0)
			{
				cargarWhiteList();
				return;
			}

			if (txtBuscarLB.Text.Length >= 2)
			{
				lvWhiteList.Items.Clear();
				foreach (string line in File.ReadAllLines(whiteListPath))
					if (line.Contains(txtBuscarLB.Text))
						lvWhiteList.Items.Add(line);
			}
		}

		private void Wait(int seconds, string comment = "Próximo en ")
		{
			int count = 0;
			//int hasta = random.Next(Convert.ToInt32(seconds / 2 + 1), seconds);
			while (count < seconds)
			{
				count++;
				this.Invoke(new Action(() =>
				{
					lSeguidos.Text = comment + (seconds - count).ToString();
					Application.DoEvents();
				}));
				Thread.Sleep(1000);
				if (driver.Url.Contains("stories"))
					actions.SendKeys(OpenQA.Selenium.Keys.Right);
			}
			this.Invoke(new Action(() =>
			{
				this.lSeguidos.Text = "A verificar: " + lvSeguidos.Items.Count.ToString();
				Application.DoEvents();
			}));
		}

		private void cbSeguro_CheckedChanged(object sender, EventArgs e)
		{
			Velocidad();
		}

		private void Velocidad()
		{
			if (cbSeguro.Checked)
			{
				delayAfterUnfollow = 120;
				delayGeneral = 120;
			}
			else
			{
				delayAfterUnfollow = 60;
				delayGeneral = 60;
			}
		}










	}
}