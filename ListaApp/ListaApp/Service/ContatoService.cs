using System;
using System.IO;
using ListaApp.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ListaApp.Service
{
    public class ContatoService
    {
        public ContatoService()
        {
            
        }

        public ObservableCollection<Contato> retorno = new ObservableCollection<Contato>();

        public const string BaseUrl = "https://5cb544bd07f233001424ceb8.mockapi.io/tete-fiap/clientes";

        public async Task<ObservableCollection<Contato>> ObterTodosContatos()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using(HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}contatos/"))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        if(string.IsNullOrWhiteSpace(result))
                        {
                            return null;
                        }
                        else
                        {

                            IList<Contato> lContato = new List<Contato>();

                            using(Stream resposeStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                            {

                                var settings = new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore,
                                    MissingMemberHandling = MissingMemberHandling.Ignore
                                };

                                lContato = JsonConvert.DeserializeObject<List<Contato>>(await new StreamReader(resposeStream)
                                    .ReadToEndAsync().ConfigureAwait(false), settings);
                            }

                            return retorno;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
            }

            return null;
        }
    }
}
