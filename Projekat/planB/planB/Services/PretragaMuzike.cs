using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Json;
using Newtonsoft.Json;
using Windows.UI.Popups;

namespace planB.Services
{
    public class PretragaMuzike
    {
        String SpotifyGET = "https://api.spotify.com/v1/search?q=";
        String SpotifyGETArtist = "&type=artist&limit=1";
        String SpotifyGETTracks = "https://api.spotify.com/v1/artists/";
        String SpotifyGETTracksAdd = "/top-tracks?country=US";

        MessageDialog Poruka;
        SearchingResult searchingResult;

        public PretragaMuzike()
        {
            searchingResult = new SearchingResult();
        }

        public async Task<SearchingResult> Search_Artists(string search)
        {
            //try
            {
                searchingResult = new SearchingResult();
                HttpClient httpClient = new HttpClient();
                String adress = SpotifyGET + search + SpotifyGETArtist;
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(new Uri(adress));
                String s = await httpResponseMessage.Content.ReadAsStringAsync();
                dynamic spotify = JsonConvert.DeserializeObject(s);
                dynamic artist = spotify["artists"];
                dynamic items = artist["items"];
                searchingResult.Artist = items[0]["name"];

                String artistID = items[0]["id"];
                httpClient = new HttpClient();
                adress = SpotifyGETTracks + artistID + SpotifyGETTracksAdd;
                httpResponseMessage = await httpClient.GetAsync(new Uri(adress));
                s = await httpResponseMessage.Content.ReadAsStringAsync();
                spotify = JsonConvert.DeserializeObject(s);
                dynamic tracks = spotify["tracks"];
                foreach (var track in tracks)
                {
                    Track newTrack = new Track();
                    //oreach (var info in track["album"])
                    {
                        newTrack.PhotoUrl = track["album"]["images"][0]["url"];
                        newTrack.Preview = track["preview_url"];
                        newTrack.Name = track["name"];
                        searchingResult.Tracks.Add(newTrack);
                    }
                }
            }
            //catch(Exception e)
            {
               // Poruka = new MessageDialog(e.Message);
                //await Poruka.ShowAsync();
            }
            return searchingResult;
        }
    }
}
