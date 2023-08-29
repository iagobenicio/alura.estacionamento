using Alura.Estacionamento.Modelos;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class UnitTest1 : IDisposable
    {
        private Veiculo _veiculo;
        private ITestOutputHelper _TestOutputHelper;
        public UnitTest1(ITestOutputHelper TestOutputHelper) 
        {
            _veiculo = new Veiculo();
            _TestOutputHelper = TestOutputHelper;

            _TestOutputHelper.WriteLine("chama construtor");
        }
        [Fact]
        public void TestaVeiculoAceelerarComParametro10()
        {
            _veiculo.Acelerar(10);

            Assert.Equal(100, _veiculo.VelocidadeAtual);


        }
        [Fact]
        public void TestaVeiculoFrearComParametro10()
        {
            //Arrange

            //Act
            _veiculo.Frear(10);
            //Assert
            Assert.Equal(-150, _veiculo.VelocidadeAtual);
        }
        [Fact]
        public void TestaTipoDoVeiculo()
        {

            _veiculo.Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Motocicleta;

            Assert.Equal(Alura.Estacionamento.Modelos.TipoVeiculo.Motocicleta, _veiculo.Tipo);

        }
        [Fact(Skip = "Teste não implementado")]
        public void ValidaProprietarioDoVeiculo() 
        {

        }

        [Fact]
        public void TestaFichaDoVeiculo() 
        {

            _veiculo.Placa = "asd-1212";
            _veiculo.Proprietario = "Iago";
            _veiculo.Cor = "Prata";
            _veiculo.Modelo = "gol";
            _veiculo.Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Automovel;

            var dadosVeiculo = _veiculo.ToString();

            Assert.Contains("Ficha do veiculo: ",dadosVeiculo);


        }

        [Fact]
        public void TestaNomeProprietarioComMenosDeTresCaracteres()
        {
            //Arrange
            string proprietario = "IA";

            //Assert
            Assert.Throws<FormatException>(() =>
            {
                //Act
                new Veiculo(proprietario);
            });

        }

        [Fact]
        public void TestaPlacaDoVeiculoComQuartoCaracterInvalido() 
        {
            string placa = "ABC*1234";

            var mensagem  = Assert.Throws<FormatException>(() => 
            {
                var veiculo = new Veiculo();
                veiculo.Placa = placa;
                return veiculo;
            });

            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }

        [Fact]
        public void TestaPlacaDoVeiculoComOsUltimosCaracteresInvalidos() 
        {
            string placa = "ABC*1b3e";

            Assert.Throws<FormatException>(() => 
            {
                var veiculo = new Veiculo();
                veiculo.Placa = placa;
                return veiculo;
            });

        }

        public void Dispose()
        {
            _TestOutputHelper.WriteLine("dispose");
        }
    }
}