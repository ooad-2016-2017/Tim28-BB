using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace planB.Services
{
    class LastFmPretraga
    {
        String lastFMLink = "http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=";
        String lastFmApiKey = "&api_key=80205c0b482bb2ae6322b02af78d1f6f&format=json";

        MessageDialog Poruka;

        SearchingResult searchingResult;

        public LastFmPretraga()
        {
            searchingResult = new SearchingResult();
        }

        public async Task<SearchingResult> Search_Artists(string search)
        {
            try
            {
                searchingResult = new SearchingResult();
                HttpClient httpClient = new HttpClient();
                String adress = lastFMLink + search + lastFmApiKey;
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(new Uri(adress));
                String s = await httpResponseMessage.Content.ReadAsStringAsync();
                dynamic spotify = JsonConvert.DeserializeObject(s);
                dynamic artist = spotify["toptracks"];
                dynamic items = artist["track"];
                searchingResult.Artist = search;


                foreach (var track in items)
                {
                    Track newTrack = new Track();
                    //oreach (var info in track["album"])
                    {
                        newTrack.PhotoUrl = track["image"][2]["#text"];
                        newTrack.Name = track["name"];
                        searchingResult.Tracks.Add(newTrack);
                    }
                }
            }
            catch(Exception e)
            {
                Poruka = new MessageDialog(e.Message);
                await Poruka.ShowAsync();
            }
            return searchingResult;
        }
    }
}
