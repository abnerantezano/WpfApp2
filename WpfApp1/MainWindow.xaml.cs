using System;
using System.Data;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly SqlConnection connection = new SqlConnection("Data Source=LAB411-011\\SQLEXPRESS;Initial Catalog=Neptuno;User ID=userPrueba;Password=123456;TrustServerCertificate=True");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BuscarProveedores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("sp_ListarProveedoresPorNombreCiudad", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NombreContacto", txtNombreContacto.Text);
                command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);

                SqlDataReader reader = command.ExecuteReader();
                List<Proveedor> proveedores = new List<Proveedor>();

                while (reader.Read())
                {
                    proveedores.Add(new Proveedor
                    {
                        IdProveedor = reader.GetInt32(0),
                        NombreCompañia = reader.GetString(1),
                        NombreContacto = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        CargoContacto = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        Direccion = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        Ciudad = reader.IsDBNull(5) ? "" : reader.GetString(5),
                        Region = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        CodPostal = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        Pais = reader.IsDBNull(8) ? "" : reader.GetString(8),
                        Telefono = reader.IsDBNull(9) ? "" : reader.GetString(9),
                        Fax = reader.IsDBNull(10) ? "" : reader.GetString(10),
                        PaginaPrincipal = reader.IsDBNull(11) ? "" : reader.GetString(11)
                    });
                }

                reader.Close();
                dgResultados.ItemsSource = proveedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar proveedores: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }


        private void BuscarPedidos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("sp_ListarDetallesPedidosPorFecha", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FechaInicio", dpInicio.SelectedDate ?? DateTime.MinValue);
                command.Parameters.AddWithValue("@FechaFin", dpFin.SelectedDate ?? DateTime.MaxValue);

                SqlDataReader reader = command.ExecuteReader();
                List<DetallePedido> detalles = new List<DetallePedido>();

                while (reader.Read())
                {
                    detalles.Add(new DetallePedido
                    {
                        IdPedido = reader.GetInt32(0),
                        IdProducto = reader.GetInt32(1),
                        PrecioUnidad = reader.GetDecimal(2),
                        Cantidad = reader.GetInt32(3),
                        Descuento = reader.GetDecimal(4),
                        FechaPedido = reader.GetDateTime(5)
                    });
                }

                reader.Close();
                dgResultados.ItemsSource = detalles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar pedidos: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }


        private void BtnListarCategorias_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("dbo.USP_ListaCategorias", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();
                List<Categoria> categorias = new List<Categoria>();

                while (reader.Read())
                {
                    categorias.Add(new Categoria
                    {
                        IdCategoria = reader.GetInt32(0),
                        NombreCategoria = reader.GetString(1),
                        Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Activo = reader.IsDBNull(3) ? null : reader.GetBoolean(3),
                        CodCategoria = reader.IsDBNull(4) ? "" : reader.GetString(4)
                    });
                }

                reader.Close();
                dgResultados.ItemsSource = categorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar categorías: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void BtnListarProductos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("dbo.USP_ListaProductos", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();
                List<Producto> productos = new List<Producto>();

                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        IdProducto = reader.GetInt32(0),
                        NombreProducto = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        IdProveedor = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                        IdCategoria = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                        CantidadPorUnidad = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        PrecioUnidad = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                        UnidadesEnExistencia = reader.IsDBNull(6) ? null : reader.GetInt16(6),
                        UnidadesEnPedido = reader.IsDBNull(7) ? null : reader.GetInt16(7),
                        NivelNuevoPedido = reader.IsDBNull(8) ? null : reader.GetInt16(8),
                        Suspendido = reader.IsDBNull(9) ? null : reader.GetInt16(9),
                        CategoriaProducto = reader.IsDBNull(10) ? "" : reader.GetString(10)
                    });
                }

                reader.Close();
                dgResultados.ItemsSource = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar productos: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void BtnListarProveedores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("dbo.USP_ListaProveedores", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();
                List<Proveedor> proveedores = new List<Proveedor>();

                while (reader.Read())
                {
                    proveedores.Add(new Proveedor
                    {
                        IdProveedor = reader.GetInt32(0),
                        NombreCompañia = reader.GetString(1),
                        NombreContacto = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        CargoContacto = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        Direccion = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        Ciudad = reader.IsDBNull(5) ? "" : reader.GetString(5),
                        Region = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        CodPostal = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        Pais = reader.IsDBNull(8) ? "" : reader.GetString(8),
                        Telefono = reader.IsDBNull(9) ? "" : reader.GetString(9),
                        Fax = reader.IsDBNull(10) ? "" : reader.GetString(10),
                        PaginaPrincipal = reader.IsDBNull(11) ? "" : reader.GetString(11)
                    });
                }

                reader.Close();
                dgResultados.ItemsSource = proveedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar proveedores: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

    }

}