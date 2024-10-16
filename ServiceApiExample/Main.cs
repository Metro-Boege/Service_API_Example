using ServiceApiExample.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ServiceApiExample
{
    public partial class Main : Form
    {
        // CONSTANTS
        // Host name and Urls can be change in the service configuration file (generally located in C:/MetroDataServer/Configuration.xml)
        private const string HostName = "http://localhost:8080/";
        private const string NewDataUrl = "api/Mesure"; // Service POST new mesure on this Url
        private const string PartChangeUrl = "api/Gamme/update"; // Service notify when part change by a POST request on this Url

        // Display elements
        private const string PartNameHeader = "Part Name :";

        // MEMBERS
        private HttpListener? Listener;

        private string PartName;
        private List<string> ColumnNames = [];

        public Main()
        {
            InitializeComponent();

            PartName = string.Empty;

            error_label.Text = "";
            partName_label.Text = PartNameHeader;
            name_label.Text = "";
            main_grid.Enabled = false;
            noData_panel.Visible = true;

            if (HttpListener.IsSupported)
            {
                Listener = new HttpListener();
                main_grid.Enabled = true;
            }
            else
            {
                error_label.Text = "HttpListener is not supported on this system.";
                Listener = null;
            }
        }

        // When window is loading
        private void Main_Load(object sender, EventArgs e)
        {
            noData_panel.Location = main_grid.Location;
            noData_panel.Size = main_grid.Size;
            main_grid.SendToBack();
            try
            {
                // Add urls to listen
                Listener?.Prefixes.Add(HostName + NewDataUrl + "/");
                Listener?.Prefixes.Add(HostName + PartChangeUrl + "/");

                // Start listening
                Listener?.Start();

                // Set callback for data reception
                Receive();
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }

        // When reset button is clicked
        private void reset_button_Click(object sender, EventArgs e)
        {
            // Reset grid
            ResetGrid();
        }

        // When window is closed
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Stop listening and close listener
            Listener?.Stop();
            Listener?.Close();
        }


        /// <summary>
        /// Set <see cref="ListenerCallback(IAsyncResult)"/> as data reception callback for the next request.
        /// </summary>
        private void Receive()
        {
            try
            {
                Listener?.BeginGetContext(new AsyncCallback(ListenerCallback), Listener);
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() =>
                {
                    WriteError(ex.Message);
                }));
            }
        }

        /// <summary>
        /// Call when request is received. Get datas from request and send back a response.
        /// <para>
        /// Since this callback is disposed after each request, it call <see cref="Receive"/> at the end to reset itself for the next request.
        /// </para>
        /// </summary>
        /// <param name="result"></param>
        private void ListenerCallback(IAsyncResult result)
        {
            try
            {
                if (Listener != null && Listener.IsListening)
                {
                    // Get request
                    HttpListenerContext context = Listener.EndGetContext(result);
                    HttpListenerRequest request = context.Request;
                    try
                    {
                        // Check which Url request commes from and whether there is any data
                        if (request.Url?.AbsolutePath.Contains(NewDataUrl) ?? false && request.HasEntityBody)
                        {
                            // Déserialize data in "PostMesureBody" object
                            JsonSerializerOptions options = new JsonSerializerOptions()
                            {
                                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
                            };

                            PostMesureBody? mesure = JsonSerializer.Deserialize<PostMesureBody>(request.InputStream, options);

                            // Add mesure in the grid
                            if (mesure != null)
                            {
                                Invoke((MethodInvoker)(() =>
                                {
                                    AddMesureToView(mesure);
                                }));
                            }

                        }
                        else if (request.Url?.AbsolutePath.Contains(PartChangeUrl) ?? false)
                        {
                            // Get device IP 
                            if (request.Url.Segments.Length > 4)
                            {
                                string ip = request.Url.Segments[4].Replace('_', '.');
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Invoke((MethodInvoker)(() =>
                        {
                            WriteError(ex.Message);
                        }));
                    }
                    // Send response to the service. NECESSARY because service can't send any request while waiting for this response                    
                    HttpListenerResponse response = context.Response;

                    // Create empty response with status 200
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.ContentType = "application/json";

                    // Send response with empty body
                    response.OutputStream.Write([], 0, 0);

                    // Close stream to end request
                    response.OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(HttpListenerException))
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        WriteError(ex.Message);
                    }));
                }
            }
            finally
            {
                // Set data reception callback
                if(Listener != null && Listener.IsListening)
                {
                    Receive();
                }                
            }
        }

        /// <summary>
        /// Display mesure in the main grid.
        /// </summary>
        /// <param name="mesure">Mesure to dispaly.</param>
        public void AddMesureToView(PostMesureBody mesure)
        {
            // Check if PartName changes and reset column if so or check for update if no.
            if (ColumnNames.Count == 0 || mesure.PartName != PartName)
            {
                CreateColumn(mesure);
            }
            else
            {
                UpdateColumn(mesure);
            }

            List<string> row = [];

            // Add data in row
            foreach (string name in ColumnNames)
            {
                if (name == "Date")
                {
                    DateTime? dateTime = null;
                    if (mesure.Date != null)
                    {
                        dateTime = new DateTime(mesure.Date.Year, mesure.Date.Month, mesure.Date.Day, mesure.Date.Hour, mesure.Date.Minute, mesure.Date.Second);
                    }

                    row.Add(dateTime.ToString() ?? "");
                }
                else if (name == "IP")
                {
                    row.Add(mesure.IpAddress ?? "");
                }
                else if (name == "Status")
                {
                    row.Add(mesure.StepStatus ?? "");
                }
                else
                {
                    row.Add(mesure.Characteristics.Find(x => x?.Name == name)?.Value.ToString() ?? "");
                }
            }

            // Add row to grid
            main_grid.Rows.Add(row.ToArray());
        }

        /// <summary>
        /// Clear data and create column from <see cref="PostMesureBody.Characteristics"/>.
        /// </summary>
        /// <param name="mesure">Mesure use to create column.</param>
        public void CreateColumn(PostMesureBody mesure)
        {
            // Clear ColumnNames
            ColumnNames.Clear();

            // Add statics columns
            ColumnNames.Add("IP");
            ColumnNames.Add("Date");

            // Add characteristics
            foreach (Characteristic? c in mesure.Characteristics)
            {
                if (c != null && c.Name != null)
                {
                    ColumnNames.Add(c.Name);
                }
            }

            // Add Status at the end
            ColumnNames.Add("Status");

            // Update PartName
            PartName = mesure.PartName ?? "";
            name_label.Text = PartName;

            // Clear grid
            main_grid.Rows.Clear();
            main_grid.Columns.Clear();

            // Add columns to grid
            foreach (string s in ColumnNames)
            {
                main_grid.Columns.Add(s, s);
            }

            main_grid.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Remove NoData panel
            noData_panel.Visible = false;
        }

        /// <summary>
        /// Check if there are new column and add them to the grid.
        /// </summary>
        /// <param name="mesure">Mesure use to add column.</param>
        public void UpdateColumn(PostMesureBody mesure)
        {
            foreach (Characteristic? c in mesure.Characteristics)
            {
                if (c != null && c.Name != null)
                {
                    // Check if column already exist
                    if (!ColumnNames.Contains(c.Name))
                    {
                        // Insert new colmun before "Status" column
                        ColumnNames.Insert(ColumnNames.Count - 2, c.Name);
                        main_grid.Columns.Insert(main_grid.Columns.Count - 2, new DataGridViewTextBoxColumn() { Name = c.Name, HeaderText = c.Name });
                    }
                }
            }
        }

        /// <summary>
        /// Write error message in <see cref="error_label"/>.
        /// </summary>
        /// <param name="text">Text to write.</param>
        public void WriteError(string text)
        {
            error_label.Text = text;
        }

        /// <summary>
        /// Clear grid and data. Add NoData panel.
        /// </summary>
        private void ResetGrid()
        {
            ColumnNames.Clear();
            main_grid.Rows.Clear();
            main_grid.Columns.Clear();

            noData_panel.Visible = true;
        }
    }
}
