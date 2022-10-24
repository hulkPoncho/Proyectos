using ConsultaBD.Transporte;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaBD.Consulta
{
    public class ConsultaCatalogos
    {
        public Dictionary<int, string> ConsultaEstados()
        {
            using(var contexto=new LocalidadesEntities())
            {
                var lista = (from a in contexto.Estado
                             select new
                             {
                                 a.estadoId,
                                 a.nombre
                             }).ToDictionary(t => t.estadoId, t => t.nombre);
                return lista;
            }
        }

        public Dictionary<int, string> ConsultaMunicipios(EntidadDTO modelo)
        {
            using (var contexto = new LocalidadesEntities())
            {
                var lista = (from a in contexto.Municipio
                             where a.estadoId==modelo.EstadoId
                             select new
                             {
                                 a.municipioId,
                                 a.nombre
                             }).ToDictionary(t => t.municipioId, t => t.nombre);
                return lista;
            }
        }

        public Dictionary<int, string> ConsultaLocalidades(EntidadDTO modelo)
        {
            using (var contexto = new LocalidadesEntities())
            {
                var lista = (from a in contexto.Localidad
                             where a.municipioId == modelo.MunicipioId
                             select new
                             {
                                 a.localidadId,
                                 a.nombre
                             }).ToDictionary(t => t.localidadId, t => t.nombre);
                return lista;
            }
        }

        public EntidadDetalleDTO ConsultaDetalle(EntidadDTO modelo)
        {
            using(var contexto=new LocalidadesEntities())
            {
                var localidad = (from a in contexto.Localidad
                                 where a.localidadId == modelo.LocalidadId
                                 select new EntidadDetalleDTO
                                 {
                                     PobFemenina = a.pobFemenina,
                                     PobMasculina = a.pobMasculina,
                                     PobTotal = a.pobTotal,
                                     TotalViviendas = a.totalViviendas
                                 }).FirstOrDefault();
                return localidad;
            }
        }

        public List<Registro> ConsultaListado(EntidadDTO modelo)
        {
            var listado = new List<Registro>();
            using (var contexto = new LocalidadesEntities())
            {
                if(modelo.LocalidadId==0 && modelo.MunicipioId == 0)
                {
                    listado = ConsultaRegistrosMunicipios(modelo, contexto);
                }
                else if(modelo.LocalidadId==0 && modelo.MunicipioId != 0)
                {
                    listado = ConsultaRegistrosLocalidades(modelo, contexto);
                }
                else
                {
                    listado = ConsultaDetalleLocalidad(modelo, contexto);

                }
                return listado;
            }
        }

        private static List<Registro> ConsultaDetalleLocalidad(EntidadDTO modelo, LocalidadesEntities contexto)
        {
            return (from a in contexto.Localidad
                    join b in contexto.Municipio on a.municipioId equals b.municipioId
                    join c in contexto.Estado on b.estadoId equals c.estadoId
                    where a.localidadId == modelo.LocalidadId
                    select new Registro
                    {
                        Localidad = a.nombre,
                        ClaveLocalidad = a.clave,
                        Entidad = c.nombre,
                        ClaveEntidad=c.clave,
                        Municipio = b.nombre,
                        ClaveMunicipio=b.clave,
                        PobFemenina = a.pobFemenina,
                        PobMasculina = a.pobMasculina,
                        PobTotal = a.pobTotal,
                        TotalViviendas = a.totalViviendas,

                    }).ToList();
        }

        private static List<Registro> ConsultaRegistrosLocalidades(EntidadDTO modelo, LocalidadesEntities contexto)
        {
            return (from a in contexto.Localidad
                    join b in contexto.Municipio on a.municipioId  equals b.municipioId
                    where a.municipioId == modelo.MunicipioId
                    select new Registro
                    {
                        Entidad = b.nombre,
                        ClaveEntidad =b.clave,
                        Municipio = a.Municipio.nombre,
                        ClaveMunicipio = a.Municipio.clave,
                        Localidad = a.nombre,
                        ClaveLocalidad = a.clave
                    }).ToList();
        }

        private static List<Registro> ConsultaRegistrosMunicipios(EntidadDTO modelo, LocalidadesEntities contexto)
        {
            return (from a in contexto.Municipio
                    where a.estadoId == modelo.EstadoId
                    select new Registro
                    {
                        Entidad=a.Estado.nombre,
                        ClaveEntidad=a.Estado.clave,
                        Municipio = a.nombre,
                        ClaveMunicipio = a.clave
                    }).ToList();
        }
    }
}
