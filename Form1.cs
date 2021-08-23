using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace Vista_y_Asignaciones
{
    public partial class Form1 : Form
    {
        private Club _club;
        private double TotalGral = 0;

        public object tbTotal { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }
        #region "Funciones suscriptas a eventos"
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla(new List<DataGridView>() { dataGridView1, dataGridView2, dataGridView3, dataGridView4 });
                _club = new Club();
                //listView1.Text = Convert.ToString(_club.ObtenerTotalGral());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Participante _auxParticipante;
                DialogResult _opcion = MessageBox.Show("Es el Participante un Socio?", "", MessageBoxButtons.YesNo);

                if (_opcion == DialogResult.Yes)
                {
                    _auxParticipante = new Socio(Interaction.InputBox("DNI: "), "", "", "So");
                }
                else
                {
                    _auxParticipante = new NoSocio(Interaction.InputBox("DNI: "), "", "", "Ns");
                }

                if (!_club.ParticipanteExiste(_auxParticipante))
                {
                    IngresaDatosParticipante(_auxParticipante); // Pido datos y asigno
                }
                else
                {
                    throw new Exception("El DNI del participante ingresado ya existe");
                }
                _club.AgregarParticipante(_auxParticipante);
                Mostrar(dataGridView1, _club.RetornaListaParticipantes());
                // Agrego a lista de torneo
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Torneo _auxTorneo = new Torneo(Interaction.InputBox("Codigo: "), "", 0.0);
                if (!_club.TorneoExiste(_auxTorneo))
                {
                    IngresaDatosTorneo(_auxTorneo);
                    _auxTorneo.FechaInicio = DateTime.Now;
                }
                else
                { throw new Exception("El Codigo de Torneo ingresado ya existe"); }
                _club.AgregarTorneo(_auxTorneo);
                Mostrar(dataGridView3, _club.RetornaListaTorneos());
                dataGridView1_RowEnter(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.SelectedRows.Count > 0)
                {
                    _club.BorrarTorneo(((Torneo)dataGridView3.SelectedRows[0].DataBoundItem));
                    Mostrar(dataGridView3, _club.RetornaListaTorneos());
                    dataGridView1_RowEnter(null, null);
                }
                else
                {
                    throw new Exception("No hay Torneos para borrar");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    Torneo _auxTorneo = ((Torneo)dataGridView1.SelectedRows[0].DataBoundItem);

                    if (_auxTorneo != null)
                    {
                        IngresaDatosTorneo(_auxTorneo);
                    }
                }
                Mostrar(dataGridView3, _club.RetornaListaTorneos());
                dataGridView1_RowEnter(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Equipo _auxEquipo = new Equipo(Interaction.InputBox("Codigo: "), ""); // Instancio objeto y completo Codigo

                if (!_club.EquipoExiste(_auxEquipo))
                {
                    IngresaDatosEquipo(_auxEquipo); // Pido datos y asigno
                }
                else
                {
                    throw new Exception("El Codigo de Equipo ingresado ya existe");
                }
                _club.AgregarEquipo(_auxEquipo);
                Mostrar(dataGridView2, _club.RetornaListaEquipos());
                // Agrego a lista de torneo
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    _club.BorrarEquipo(((Equipo)dataGridView2.SelectedRows[0].DataBoundItem));
                    Mostrar(dataGridView2, _club.RetornaListaEquipos());
                }
                else
                {
                    throw new Exception("No hay Equipos para borrar");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    Equipo _auxEquipo = ((Equipo)dataGridView2.SelectedRows[0].DataBoundItem);
                    if (_auxEquipo != null)
                    {
                        IngresaDatosEquipo(_auxEquipo);
                    }
                }
                Mostrar(dataGridView2, _club.RetornaListaEquipos());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.SelectedRows.Count > 0)
                {
                    _club.BorrarParticipante(((Participante)dataGridView1.SelectedRows[0].DataBoundItem));
                    Mostrar(dataGridView1, _club.RetornaListaParticipantes());
                }
                else
                {
                    throw new Exception("No hay Participantes para borrar");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    Participante _auxParticipante = ((Participante)dataGridView3.SelectedRows[0].DataBoundItem);
                    if (_auxParticipante != null)
                    {
                        IngresaDatosParticipante(_auxParticipante);
                    }
                }
                Mostrar(dataGridView1, _club.RetornaListaParticipantes());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                Deporte _auxDeporte = new Deporte(Interaction.InputBox("Codigo: "), ""); // Instancio objeto y completo Codigo

                if (!_club.DeporteExiste(_auxDeporte))
                {
                    IngresaDatosDeporte(_auxDeporte); // Pido datos y asigno
                }
                else
                {
                    throw new Exception("El Codigo de deporte ingresado existe");
                }
                _club.AgregarDeporte(_auxDeporte);
                Mostrar(dataGridView4, _club.RetornaListaDeportes());
                // Agrego a lista de Deporte
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.SelectedRows.Count > 0)
                {
                    _club.BorrarDeporte(((Deporte)dataGridView4.SelectedRows[0].DataBoundItem));
                    Mostrar(dataGridView4, _club.RetornaListaDeportes());
                }
                else
                {
                    throw new Exception("No hay Deportes para borrar");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.SelectedRows.Count > 0)
                {
                    Deporte _auxDeporte = ((Deporte)dataGridView4.SelectedRows[0].DataBoundItem);
                    if (_auxDeporte != null)
                    {
                        IngresaDatosDeporte(_auxDeporte);
                    }
                }
                Mostrar(dataGridView4, _club.RetornaListaDeportes());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0 && dataGridView2.Rows.Count > 0 && dataGridView2.SelectedRows.Count > 0)
                {
                    Torneo _auxTorneo;
                    Equipo _auxEquipo;

                    _auxTorneo = (Torneo)dataGridView1.SelectedRows[0].DataBoundItem;
                    _auxEquipo = (Equipo)dataGridView2.SelectedRows[0].DataBoundItem;
                    if (_auxEquipo.TorneoPertenece() is null)
                    {
                        _auxTorneo.AgregarEquipo(_auxEquipo);
                        _auxEquipo.AsignarTorneo(_auxTorneo);
                        dataGridView1_RowEnter(null, null);
                    }
                    else throw new Exception("El equipo esta asignado");
                }
                else
                {
                    throw new Exception("Alguna de las grillas no poseen elementos");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try

            {
                if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0 && dataGridView4.Rows.Count > 0 && dataGridView4.SelectedRows.Count > 0)
                {
                    Torneo _auxTorneo;
                    Deporte _auxDeporte;

                    _auxTorneo = (Torneo)dataGridView1.SelectedRows[0].DataBoundItem;
                    _auxDeporte = (Deporte)dataGridView4.SelectedRows[0].DataBoundItem;

                    Torneo _auxTorneoDel = _auxDeporte.RetornaListaTorneos().Find(x => x.Codigo == dataGridView4.SelectedRows[0].Cells[0].Value.ToString());
                    _auxDeporte.RetornaListaTorneos().Remove(_auxTorneoDel);
                    _auxTorneo.AsignarDeporte(null);
                    dataGridView1_RowEnter(null, null);
                }
                else throw new Exception("Alguna de las grillas no poseen elementos");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            AsignarTotalGeneral(0);
            TorneoVistaInic _tvi = new TorneoVistaInic();
            TorneoVistaFin _tvf = new TorneoVistaFin();
            try
            {
                //if (dataGridView5.Rows.Count > 0)
                //{
                Mostrar(dataGridView5, _tvf.RetornaListaTorneoVistaFin(_club.RetornaListaTorneos()));
                //}
                //if (dataGridView6.Rows.Count > 0)
                //{
                Mostrar(dataGridView6, _tvi.RetornaListaTorneoVistaInic(_club.RetornaListaTorneos()));
                //}
            }
            catch (Exception) { }
        }
        #endregion
        #region "Funciones privadas de Servicio"
        //Seteo configuracion visual de grillas
        private void ConfigurarGrilla(List<DataGridView> _pLDGV)
        {
            foreach (DataGridView _dataGridView in _pLDGV)
            {
                _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                _dataGridView.MultiSelect = false;
                _dataGridView.EnableHeadersVisualStyles = false;
                _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
        }
        //Cargo datos de torneo
        private void IngresaDatosTorneo(Torneo _auxTorneo)
        {
            _auxTorneo.Nombre = Interaction.InputBox("Nombre: ", "", _auxTorneo.Nombre);
            _auxTorneo.Costo = Convert.ToDouble(Interaction.InputBox("Costo: ", "", Convert.ToString(_auxTorneo.Costo)));

            DialogResult _opcion = MessageBox.Show("Está el torneo finalizado?", "", MessageBoxButtons.YesNo);
            if (_opcion == DialogResult.Yes)
            {
                _auxTorneo.Finalizado = true;
            }
            else if (_opcion == DialogResult.No)
            {
                _auxTorneo.Finalizado = false;
            }

        }
        //Cargo datos de equipo
        private void IngresaDatosEquipo(Equipo _auxEquipo)
        {
            _auxEquipo.Nombre = Interaction.InputBox("Nombre: ", "", _auxEquipo.Nombre);
        }
        //Cargo datos de participante
        private void IngresaDatosParticipante(Participante _auxParticipante)
        {
            _auxParticipante.Nombre = Interaction.InputBox("Nombre: ", "", _auxParticipante.Nombre);
            _auxParticipante.Apellido = Interaction.InputBox("Apellido: ", "", _auxParticipante.Apellido);
        }
        //Cargo datos de deporte
        private void IngresaDatosDeporte(Deporte _auxDeporte)
        {
            _auxDeporte.Nombre = Interaction.InputBox("Nombre: ", "", _auxDeporte.Nombre);
        }
        //Muestro Lista en Grilla
        private void Mostrar(DataGridView pDGV, object pQueMuestro)
        {
            pDGV.DataSource = null; pDGV.DataSource = pQueMuestro;
            //dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        //Asigno total general recaudado
        public void AsignarTotalGeneral(double pMonto) { TotalGral = pMonto; }
        //Calculo total general recaudado
        public void CalcularTotalGeneral(double pMonto)
        {
            try
            {
                TotalGral += pMonto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Recupero total general recaudado
       
        #endregion
        // Clase usuaria del sistema
        public class Club
        {
            #region "Campos"
            private List<Torneo> _lTorneos;
            private List<Equipo> _lEquipos;
            private List<Participante> _lParticipantes;
            private List<Deporte> _lDeportes;

            #endregion
            #region "Constructores"
            public Club()
            {
                _lTorneos = new List<Torneo>();
                _lEquipos = new List<Equipo>();
                _lParticipantes = new List<Participante>();
                _lDeportes = new List<Deporte>();
            }
            #endregion
            #region "Propiedades"
            public string Codigo { get; set; }
            public string Nombre { get; set; }

            #endregion
            #region "Métodos"
            public List<Torneo> RetornaListaTorneos() { return _lTorneos; }
            public List<Equipo> RetornaListaEquipos() { return _lEquipos; }
            public List<Participante> RetornaListaParticipantes() { return _lParticipantes; }
            public List<Deporte> RetornaListaDeportes() { return _lDeportes; }
            //Consulta si un torneo ya existe en la lista
            public bool TorneoExiste(Torneo pTorneo)
            { return _lTorneos.Exists(x => x.Codigo == pTorneo.Codigo); }
            //Agrego torneo a la lista
            public void AgregarTorneo(Torneo pTorneo)
            {
                try
                {
                    if (!TorneoExiste(pTorneo))
                    {
                        _lTorneos.Add(pTorneo);
                    }
                    else
                    {
                        throw new Exception("El Torneo ingresado existe");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //Borro torneo de la lista
            public void BorrarTorneo(Torneo pTorneo)
            {
                try
                {
                    _lTorneos.Remove(pTorneo);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //Consulta si un equipo ya existe en la lista
            public bool EquipoExiste(Equipo pEquipo)
            { return _lEquipos.Exists(x => x.Codigo == pEquipo.Codigo); }
            //Agrego equipo a la lista
            public void AgregarEquipo(Equipo pEquipo)
            {
                try
                {
                    if (!EquipoExiste(pEquipo))
                    {
                        _lEquipos.Add(pEquipo);
                    }
                    else
                    {
                        throw new Exception("El Equipo ingresado existe");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //Borro equipo de la lista
            public void BorrarEquipo(Equipo pEquipo)
            {
                try
                {
                    _lEquipos.Remove(pEquipo);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //Consulta si un participante ya existe en la lista
            public bool ParticipanteExiste(Participante pParticipante)
            { return _lParticipantes.Exists(x => x.DNI == pParticipante.DNI); }
            //Agrego participante a la lista
            public void AgregarParticipante(Participante pParticipante)
            {
                try
                {
                    if (!ParticipanteExiste(pParticipante))
                    {
                        _lParticipantes.Add(pParticipante);
                    }
                    else
                    {
                        throw new Exception("El Participante ingresado existe");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //Borro participante de la lista
            public void BorrarParticipante(Participante pParticipante)
            {
                try
                {
                    _lParticipantes.Remove(pParticipante);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //Consulta si un deporte ya existe en la lista
            public bool DeporteExiste(Deporte pDeporte)
            { return _lDeportes.Exists(x => x.Codigo == pDeporte.Codigo); }
            //Agrego deporte a la lista
            public void AgregarDeporte(Deporte pDeporte)
            {
                try
                {
                    if (!DeporteExiste(pDeporte))
                    {
                        _lDeportes.Add(pDeporte);
                    }
                    else
                    {
                        throw new Exception("El Deporte ingresado existe");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //Borro deporte de la lista
            public void BorrarDeporte(Deporte pDeporte)
            {
                try
                {
                    _lDeportes.Remove(pDeporte);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion
            #region "Destructor"
            #endregion
        }
        public class Torneo
        {
            #region "Campos"
            private List<Equipo> _lEquipos;
            Deporte _deporte;
            #endregion
            #region "Constructores"
            public Torneo()
            {
                _lEquipos = new List<Equipo>();
            }
            public Torneo(string pCodigo, string pNombre, double pCosto) : this()
            {
                Codigo = pCodigo;
                Nombre = pNombre;
                Costo = pCosto;
            }
            #endregion
            #region "Propiedades"
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public DateTime FechaInicio { get; set; }
            public bool Finalizado { get; set; }
            public double Costo { get; set; }
            #endregion
            #region "Métodos"
            public List<Equipo> RetornaListaEquipos() { return _lEquipos; }
            public void AgregarEquipo(Equipo pEquipo)
            {
                try
                {
                    if (!_lEquipos.Exists(x => x.Codigo == pEquipo.Codigo))
                    {
                        _lEquipos.Add(pEquipo);
                    }
                    else
                    {
                        throw new Exception("Equipo ya asignado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            public Deporte DeportePertenece() { return _deporte; }
            public void AsignarDeporte(Deporte pDeporte) { _deporte = pDeporte; }
            public int ObtenerCantParticipantes()
            {
                int _cant = 0;
                foreach (Equipo _e in _lEquipos)
                {
                    _cant += _e.RetornaListaParticipantes().Count;
                }
                return _cant;
            }
            public double CalcularTotalTorneo(double pCosto)
            {
                //string _total = "";
                double _auxTotal = 0;

                foreach (Equipo _e in _lEquipos)
                {
                    _auxTotal += _e.CalcularTotalParticipantes(pCosto);
                }
                //_total = Convert.ToString(_auxTotal); 
                return _auxTotal;
            }
            #endregion
            #region "Destructor"
            ~Torneo() { _lEquipos = null; MessageBox.Show($"El codigo del torneo es: {Codigo}"); }
            #endregion
        }
        public class Equipo
        {
            #region "Campos"
            private List<Participante> _lParticipantes;
            Torneo _torneo;
            #endregion
            #region "Constructores"
            public Equipo() { _lParticipantes = new List<Participante>(); }
            public Equipo(string pCodigo, string pNombre) : this()
            { Codigo = pCodigo; Nombre = pNombre; }
            #endregion
            #region "Propiedades"
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            #endregion
            #region "Métodos"
            public Torneo TorneoPertenece() { return _torneo; }
            public void AsignarTorneo(Torneo pTorneo) { _torneo = pTorneo; }
            public List<Participante> RetornaListaParticipantes() { return _lParticipantes; }
            public void AgregarParticipante(Participante pParticipante)
            {
                try
                {
                    if (!(_lParticipantes.Exists(x => x.DNI == pParticipante.DNI)))
                    {
                        _lParticipantes.Add(pParticipante);
                    }
                    else
                    {
                        throw new Exception("Participante ya asignado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            public double CalcularTotalParticipantes(double pCosto)
            {
                try
                {
                    double _auxTotalEquipo = 0;
                    foreach (Participante _p in _lParticipantes)
                    {
                        _auxTotalEquipo += _p.ObtenerInscripcion(pCosto);
                    }
                    return _auxTotalEquipo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }
            #endregion
            #region "Destructor"
            #endregion
        }
        public class Participante
        {
            #region "Campos"
            //private List<Equipo> _lEquipos;
            Equipo _equipo;
            #endregion
            #region "Constructores"
            public Participante()
            {
                //_lEquipos = new List<Equipo>();
            }
            public Participante(string pDNI, string pNombre, string pApellido, string pSoNs) : this()
            {
                DNI = pDNI;
                Nombre = pNombre;
                Apellido = pApellido;
                SoNs = pSoNs;
            }
            #endregion
            #region "Propiedades"
            public string DNI { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string SoNs { get; set; }
            #endregion
            #region "Métodos"
            public Equipo EquipoPertenece() { return _equipo; }
            public void AsignarEquipo(Equipo pEquipo) { _equipo = pEquipo; }
            //public int CalcularCosto(Equipo pEquipo) { _equipo = pEquipo; }
            virtual public double ObtenerInscripcion(double pCosto)
            {
                double _CostoPart = 0;
                _CostoPart = 1 * pCosto;
                return _CostoPart;
            }
            #endregion
            #region "Destructor"
            #endregion
        }
        public class NoSocio : Participante
        {
            #region "Campos"
            #endregion
            #region "Constructores"
            public NoSocio(string pDNI, string pNombre, string pApellido, string pSoNs) : base(pDNI, pNombre, pApellido, pSoNs)
            {
                DNI = pDNI;
                Nombre = pNombre;
                Apellido = pApellido;
                SoNs = pSoNs;
            }
            #endregion
            #region "Propiedades"
            #endregion
            #region "Métodos"
            #endregion
            #region "Destructor"
            #endregion
        }
        public class Socio : Participante
        {
            #region "Campos"
            #endregion
            #region "Constructores"
            public Socio(string pDNI, string pNombre, string pApellido, string pSoNs) : base(pDNI, pNombre, pApellido, pSoNs)
            {
                DNI = pDNI;
                Nombre = pNombre;
                Apellido = pApellido;
                SoNs = pSoNs;
            }
            #endregion
            #region "Propiedades"
            #endregion
            #region "Métodos"
            //Redefino metodo para calcular el valor de inscripcion con descuento
            public override double ObtenerInscripcion(double pCosto)
            {
                double _CostoPart = 0;
                _CostoPart = 0.8 * pCosto;
                return _CostoPart;
            }
            #endregion
            #region "Destructor"
            #endregion
        }
        public class Deporte
        {
            #region "Campos"
            private List<Torneo> _lTorneos;
            #endregion
            #region "Constructores"
            public Deporte()
            {
                _lTorneos = new List<Torneo>();
            }
            public Deporte(string pCodigo, string pNombre) : this()
            {
                Codigo = pCodigo;
                Nombre = pNombre;
            }
            #endregion
            #region "Propiedades"
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            #endregion
            #region "Métodos"
            public List<Torneo> RetornaListaTorneos() { return _lTorneos; }
            public void AgregarTorneo(Torneo pTorneo)
            {
                try
                {
                    if (!_lTorneos.Exists(x => x.Codigo == pTorneo.Codigo))
                    {
                        _lTorneos.Add(pTorneo);
                    }
                    else
                    {
                        throw new Exception("Torneo ya fue asignado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            #endregion
            #region "Destructor"
            #endregion
        }
        abstract public class TorneoVista
        {
            #region "Campos"
            #endregion
            #region "Constructores"
            public TorneoVista() { }
            public TorneoVista(string pCodigo, string pNombre, int pCantEquip, int pCantPart, string pDeporte, double pTotal) : this()
            {
                Codigo = pCodigo;
                Nombre = pNombre;
                CantEquip = pCantEquip;
                CantPart = pCantPart;
                Deporte = pDeporte;
                Total = pTotal;
            }
            #endregion
            #region "Propiedades"
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public int CantEquip { get; set; }
            public int CantPart { get; set; }
            public string Deporte { get; set; }
            public double Total { get; set; }
            #endregion
            #region "Métodos"
            #endregion
            #region "Destructor"
            #endregion
        }
        public class TorneoVistaInic : TorneoVista
        {
            #region "Campos"
            private List<TorneoVistaInic> _lTorneoVistaInic;
            #endregion
            #region "Constructores"
            public TorneoVistaInic()
            {
                _lTorneoVistaInic = new List<TorneoVistaInic>();
            }
            public TorneoVistaInic(string pCodigo, string pNombre, DateTime pFechaInic, int pCantEquip, int pCantPart, string pDeporte, double pTotal) : base(pCodigo, pNombre, pCantEquip, pCantPart, pDeporte, pTotal)
            {
                Codigo = pCodigo;
                Nombre = pNombre;
                FechaInic = pFechaInic;
                CantEquip = pCantEquip;
                CantPart = pCantPart;
                Deporte = pDeporte;
                Total = pTotal;
            }
            #endregion
            #region "Propiedades"
            public DateTime FechaInic { get; set; }
            #endregion
            #region "Métodos"
            //Defino la lista a cargar en torneos iniciados
            public List<TorneoVistaInic> RetornaListaTorneoVistaInic(List<Torneo> pListaTorneo)
            {
                _lTorneoVistaInic.Clear();
                try
                {
                    foreach (Torneo _t in pListaTorneo)
                    {
                        if (_t.Finalizado == false)
                        {
                            Deporte auxDeporte = _t.DeportePertenece();

                            _lTorneoVistaInic.Add(new TorneoVistaInic(_t.Codigo, _t.Nombre, _t.FechaInicio, _t.RetornaListaEquipos().Count, _t.ObtenerCantParticipantes(), _t.DeportePertenece()?.Nombre, _t.CalcularTotalTorneo(_t.Costo)));
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                return _lTorneoVistaInic;
            }
            #endregion
            #region "Destructor"
            #endregion
        }
        public class TorneoVistaFin : TorneoVista
        {
            #region "Campos"
            private List<TorneoVistaFin> _lTorneoVistaFin;
            #endregion
            #region "Constructores"
            public TorneoVistaFin()
            {
                _lTorneoVistaFin = new List<TorneoVistaFin>();
            }
            public TorneoVistaFin(string pCodigo, string pNombre, int pCantEquip, int pCantPart, string pDeporte, double pTotal) : base(pCodigo, pNombre, pCantEquip, pCantPart, pDeporte, pTotal)
            {
                Codigo = pCodigo;
                Nombre = pNombre;
                CantEquip = pCantEquip;
                CantPart = pCantPart;
                Deporte = pDeporte;
                Total = pTotal;
            }
            #endregion
            #region "Propiedades"
            public DateTime FechaInic { get; set; }
            #endregion
            #region "Métodos"
            //Defino la lista a cargar en torneos finalizados
            public List<TorneoVistaFin> RetornaListaTorneoVistaFin(List<Torneo> pListaTorneo)
            {
                _lTorneoVistaFin.Clear();
                try
                {
                    foreach (Torneo _t in pListaTorneo)
                    {
                        if (_t.Finalizado == true)
                        {
                            Deporte auxDeporte = _t.DeportePertenece();
                            _lTorneoVistaFin.Add(new TorneoVistaFin(_t.Codigo, _t.Nombre, _t.RetornaListaEquipos().Count, _t.ObtenerCantParticipantes(), _t.DeportePertenece()?.Nombre, _t.CalcularTotalTorneo(_t.Costo)));
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                return _lTorneoVistaFin;
            }
            #endregion
            #region "Destructor"
            #endregion
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
       
            { listView1.Text = Convert.ToString(TotalGral); }
        
    }
}
    

 

