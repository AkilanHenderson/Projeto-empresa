﻿using Projeto_empresa.DAOs;
using Projeto_empresa.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_empresa.Controllers
{
    public class FuncionarioController
    {
        private readonly FuncionarioDAO _funcionarioDAO;

        // Construtor que recebe o DAO
        public FuncionarioController(FuncionarioDAO funcionarioDAO)
        {
            _funcionarioDAO = funcionarioDAO;
        }

        // Método para cadastrar um funcionário
        public void CadastrarFuncionario(FuncionarioDTO funcionario)
        {
            // Aqui você pode adicionar validações ou regras de negócio
            _funcionarioDAO.Insert(funcionario);
        }

        // Método para listar todos os funcionários
        public List<FuncionarioDTO> ListarFuncionarios()
        {
            return _funcionarioDAO.GetAll();
        }
    }
}
