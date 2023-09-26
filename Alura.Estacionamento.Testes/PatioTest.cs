using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Alura.Estacionamento.Modelos;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTest : IDisposable
    {
        private Veiculo _veiculo;
        private ITestOutputHelper _TestOutputHelper;
        public PatioTest(ITestOutputHelper TestOutputHelper) 
        {
            _veiculo = new Veiculo();
            _TestOutputHelper = TestOutputHelper;

            _TestOutputHelper.WriteLine("chama construtor");
        }
        [Fact]
        public void ValidaFaturamentoDoVeiculoEstacionadoNoPatio() 
        {
            //Arrange

            var operador = new Operador();
            var patio = new Patio();

            patio.OperadorPatio = operador;
            _veiculo.Proprietario = "Iago";
            _veiculo.Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Automovel;
            _veiculo.Cor = "Preto";
            _veiculo.Modelo = "Qualquer";
            _veiculo.Placa = "aaa-4589";

            patio.RegistrarEntradaVeiculo(_veiculo);
            patio.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act 
            double faturamento = patio.TotalFaturado();

            //Assert 
            Assert.Equal(2,faturamento);

        }
        [Theory]
        [InlineData("Iago","asd-1926","Gol")]
        [InlineData("Maria", "asd-1923", "Gol")]
        [InlineData("José", "asd-2123", "Gol")]
        public void ValidaFaturamentoDeVariosVeiculosEstacionadosNoPatio(string proprietario,string placa,string modelo) 
        {

            //Arrange
           
            var operador = new Operador();
            var patio = new Patio();

            patio.OperadorPatio = operador;
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Automovel;
            _veiculo.Cor = "Preto";
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            patio.RegistrarEntradaVeiculo(_veiculo);
            patio.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act 
            double faturamento = patio.TotalFaturado();

            //Assert 
            Assert.Equal(2, faturamento);


        }

        [Theory]
        [InlineData("Iago", "asd-1926", "Gol")]
        public void PesquisaVeiculoAtravesDoTicket(string proprietario, string placa, string modelo) 
        {


            var patio = new Patio();
            var OP = new Operador();

            patio.OperadorPatio = OP;
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Automovel;
            _veiculo.Cor = "Preto";
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            patio.RegistrarEntradaVeiculo(_veiculo);

            var veiculoResult = patio.PesquisaVeiculoPorTicket(_veiculo.Ticket);

            Assert.Contains("### Ticket Estacionameno Alura ###", _veiculo.Ticket);

        }
        [Theory]
        [InlineData("Iago", "asd-1926", "Gol")]
        public void AlteraDadosDoVeiculoEstacionadoNoPatio(string proprietario, string placa, string modelo) 
        {
            //Arrange
            var operador = new Operador();
            var patio = new Patio();

            patio.OperadorPatio = operador;
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Automovel;
            _veiculo.Cor = "Preto";
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;
            patio.RegistrarEntradaVeiculo(_veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = proprietario;
            veiculoAlterado.Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Automovel;
            veiculoAlterado.Cor = "Azul";
            veiculoAlterado.Modelo = modelo;
            veiculoAlterado.Placa = placa;

            //Act
            Veiculo veiculoAlteradoResultado = patio.AlteraDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(veiculoAlteradoResultado.Cor, veiculoAlterado.Cor);

        }

        [Fact]
        public void TestaGerarTicket() 
        {
            var operador = new Operador();
            var patio = new Patio();

            patio.OperadorPatio = operador;

            _veiculo.Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Automovel;
            _veiculo.Cor = "Preto";
            _veiculo.Proprietario = "Iago";
            _veiculo.Modelo = "Gol";
            _veiculo.Placa = "asd-1926";
            _veiculo.Ticket = null;

            patio.RegistrarEntradaVeiculo(_veiculo);

            Assert.NotNull(_veiculo.Ticket);
            Assert.Contains("asd-1926",_veiculo.Ticket);

        }
        public void Dispose()
        {
            _TestOutputHelper.WriteLine("dispose");
        }
    }
}
