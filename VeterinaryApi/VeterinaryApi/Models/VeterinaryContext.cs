using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VeterinaryApi.Models
{
    public class VeterinaryContext : DbContext
    {
        public VeterinaryContext(DbContextOptions<VeterinaryContext> options)
            : base(options)
        {
        }

        public DbSet<Rol> roles { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Mascota> mascotas { get; set; }
        public DbSet<HorarioAtencion> horarios_atencion { get; set; }
        public DbSet<EstadoCita> estados_cita { get; set; }
        public DbSet<TipoAtencion> tipo_atencion_o_servicio { get; set; }
        public DbSet<AtencionServicio> atenciones_y_servicios { get; set; }
        public DbSet<Vacuna> vacunas { get; set; }
        public DbSet<CarnetVacunacion> carnet_vacunaciones { get; set; }
        public DbSet<Diagnostico> diagnosticos { get; set; }
        public DbSet<Factura> facturas { get; set; }
        public DbSet<EstadoPago> estados_pago { get; set; }
        public DbSet<TipoPago> tipos_pago { get; set; }
        public DbSet<DetallePago> detalle_pagos { get; set; }
    }

    public class Rol
    {
        [Key]
        public int id { get; set; }
        public int idroles { get; set; }
        public required string nombre_rol { get; set; }
    }

    public class Usuario
    {
        [Key]
        public int idusuarios { get; set; }
        public required string nombre_usuario { get; set; }
        public required string correo_ususario { get; set; }
        public required string telefono_usuario { get; set; }
        public required string direccion_usuario { get; set; }
        public required string contraseña_usuario { get; set; }
        public int Roles_idroles { get; set; }
    }

    public class Mascota
    {
        [Key]
        public int idmascotas { get; set; }
        public required string nombre_mascota { get; set; }
        public int edad_mascota { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public int clientes_idclientes { get; set; }
        public required string tipo_animal { get; set; }
        public required string raza_animal { get; set; }
        public int usuarios_dueno_idusuarios { get; set; }
    }

    public class HorarioAtencion
    {
        [Key]
        public int idhorarios_atencion { get; set; }
        public TimeSpan hora_inicio { get; set; }
        public TimeSpan hora_fin { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
    }

    public class EstadoCita
    {
        [Key]
        public int idestados_cita { get; set; }
        public required string nombre_estado { get; set; }
    }

    public class TipoAtencion
    {
        [Key]
        public int idatencion_o_servicio { get; set; }
        public required string nombre_tipo { get; set; }
        public decimal precio_tipo { get; set; }
    }

    public class AtencionServicio
    {
        [Key]
        public int idcitas { get; set; }
        public DateTime fecha_apartada { get; set; }
        public TimeSpan hora_inicio { get; set; }
        public TimeSpan hora_final { get; set; }
        public int estados_cita_idestados_cita { get; set; }
        public int tipo_atencion_o_servicio_idatencion_o_servicio { get; set; }
        public int usuarios_veterinario_idusuarios { get; set; }
        public int mascotas_idmascotas { get; set; }
    }

    public class Vacuna
    {
        [Key]
        public int idvacunas { get; set; }
        public required string nombre_vacuna { get; set; }
        public required string dosis_vacuna { get; set; }
        public required string descripción_vacuna { get; set; }
    }

    public class CarnetVacunacion
    {
        [Key]
        public int id { get; set; }

        public int vacunas_idvacunas { get; set; }
        public DateTime fecha_hora_vacunacion { get; set; }
        public int mascotas_idmascotas { get; set; }
        public int usuarios_veterinario_idusuarios { get; set; }
    }

    public class Diagnostico
    {
        [Key]
        public int iddiagnosticos { get; set; }
        public required string descripcion_diagnostico { get; set; }
        public int atenciones_y_servicios_idcitas { get; set; }

    }

    public class Factura
    {
        [Key]
        public int idfacturas { get; set; }
        public int usuarios_idusuarios { get; set; }
    }

    public class EstadoPago
    {
        [Key]
        public int idestados_pago { get; set; }
        public required string nombre_pago { get; set; }
    }

    public class TipoPago
    {
        [Key]
        public int idtipos_pago { get; set; }
        public required string nombre_tipo_pago { get; set; }
    }

    public class DetallePago
    {
        [Key]
        public int iddetalle_pagos { get; set; }
        public DateTime fecha_hora_pago { get; set; }
        public decimal monto_pago { get; set; }
        public required string detalle_pagoscol { get; set; }
        public int estados_pago_idestados_pago { get; set; }
        public int tipos_pago_idtipos_pago { get; set; }
        public int facturas_idfacturas { get; set; }
    }
}
