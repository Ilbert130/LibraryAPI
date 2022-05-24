using AutoMapper;
using PruebeVC.DTOs;
using PruebeVC.Models;

namespace PruebeVC.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<LibroCreacionDTO, Libro>();
            CreateMap<Libro,LibroDTO>();
        }
    }
}