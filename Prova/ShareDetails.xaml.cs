using Dominio.Entidades;
using Newtonsoft.Json;
using Integracao;

namespace Prova;

public partial class ShareDetails : ContentPage
{
    private string _shareSymbol;
    private readonly BaseClient _client = new BaseClient();

    public ShareDetails(string shareSymbol)
    {
        InitializeComponent(); // Isso deve vir primeiro
        _shareSymbol = shareSymbol;
        ShowShareDetails(_shareSymbol);
    }

    public async Task ShowShareDetails(string shareSymbol)
    {
        try
        {
            HttpResponseMessage respostaAPI = await _client.GetShare(shareSymbol);
            string conteudo = await respostaAPI.Content.ReadAsStringAsync();
            Acao acao = JsonConvert.DeserializeObject<Acao>(conteudo);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Logotipo.Source = acao.Logourl;
                ShortName.Text = $"A��o = {acao.ShortName}";
                LongName.Text = $"Empresa = {acao.LongName}";
                Marketcap.Text = $"Capitaliza��o de mercado = R${acao.MarketCap}";
                Value.Text = $"Valor atual = R${(acao.RegularMarketPrice)}";
                RegularMarketChange.Text = $"Varia��o do dia = R${(acao.RegularMarketChange)}";
            });
        }
        catch (Exception ex)
        {
            // Tratar exce��o (por exemplo, mostrando uma mensagem para o usu�rio)
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Atualizar a UI para refletir o erro
            });
        }
    }
}