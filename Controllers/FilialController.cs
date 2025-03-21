﻿using Projeto_empresa.DAOs;
using Projeto_empresa.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_empresa.Controllers
{
    public class FilialController
    {
        private readonly FilialDAO _filialDAO;

        // Construtor que recebe o DAO
        public FilialController(FilialDAO filialDAO)
        {
            _filialDAO = filialDAO;
        }

        // Método para cadastrar uma filial
        public void CadastrarFilial(FilialDTO filial)
        {
            // Aqui você pode adicionar validações ou regras de negócio
            _filialDAO.Insert(filial);
        }

        // Método para listar todas as filiais
        public List<FilialDTO> ListarFiliais()
        {
            return _filialDAO.GetAll();
        }

        // Método para listar apenas Id e Nome das filiais (para o ComboBox)
        public List<FilialDTO> ListarFiliaisParaComboBox()
        {
            return _filialDAO.GetAllParaComboBox();
        }

        // Método para validar o login
        public bool ValidarLogin(string email, string senha)
        {
            return _filialDAO.ValidarLogin(email, senha);
        }

        // Método para buscar um funcionário por e-mail
        public FilialDTO BuscarFilialPorEmail(string email)
        {
            return _filialDAO.BuscarFilialPorEmail(email);
        }

        //metodo para atualizar filial
        public void AtualizarFilial(FilialDTO filial)
        {
            _filialDAO.AtualizarFilial(filial);
        }

        //metodo para deletar a filial
        public void DeletarFilial(int id)
        {
            _filialDAO.DeletarFilial(id);
        }
    }
}
