using HtmlAgilityPack;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Dota_Picking_Partner.Models;
using System.Reflection;
using Google.Type;

namespace Dota_Picking_Partner.Data
{
    public class DataLayer
    {
         static readonly FirestoreClientBuilder builder = new FirestoreClientBuilder
        {
            JsonCredentials = @"
                {
                    ""type"": ""service_account"",
                    ""project_id"": ""dota-picking-partner"",
                    ""private_key_id"": ""080c9e37cd62a037e040de303ca66684427af6f8"",
                    ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDDs3bKkwhnL8g5\nC0oisZ6aXumxLl0mEN6UVrRl9xw2b5p/x6NUtrvqqD1f9kp9ydtG/VzXtNWr9dj5\nJGoxTPDdj62mJBU4szqh9oheRw0whETalCgA+H/0D4bgFRtcXW8ud+svqxHEkEJ9\n9MKGN8gIjCuNe2PQiGRlbQT9FF3FfMa7mTBdxLOIEH8e32myHmZ2BdzCaSS+bODX\nJ6t8dv6tX49j7PJteaByKsEBfqfN/X4HsYiNzAnhTsfSOHQc1vuJnlQ9rvUnM7kR\nN2608j1W9pNLLDXnNRAoQ/tw3ckwYJu+hc9Nttw0n60iFR9iyy3T+sWs75rtzpYA\n8M5pYsdhAgMBAAECggEACnQJeDpYmG9pU9+2IOqJKOCBPdM2y0fbJcQIXIpxpbg6\nIdFreGTO3pLIPXoZwkSQG4eAyC5a+/WUtoEjEXlVQ9Zu/Xj/r7DGJNzW7adArXrh\nNSY1CVE8v2/9YbaK6Lj+bXZfGAOcnjng7VqvsczkuM0o7eSdgSfp6e+D9NM7y4a9\nvLQL3Xkl1n8FmxGkddDmfTONufaIc7OdId8o7eykMchfztYnaFfWPaN8GMI9NS+C\nlqqh02l5NWaZFuErq1lBp2gPHom17EMk8o9o8iTADrC2XcR9xUMcN6KT4BN4R2AD\nWCvIKW2hvDBBb5Bsf+yMM0PqNqQM6fM6EM12WFXS+QKBgQD9D2OnUBdOkCR42rLX\nOR0p+eLwwDTrL+CmlfGRv/sTgDhyDICYo8NgWj3MTrQ1UxHkUei5AvMilQ694X30\ngE5eSw0KYOTWy2WD+oxZxN8d/JzwFP9HKWHHDxuB0/e5QECUzzwzPQc4NdugMn2n\nbfyWXQ0YXqIYzoS93eppZf32GQKBgQDF+XyRGvxqc6igvxGhfqe4q47W33Uf5gZE\nEs6w9mglih0wk9cjklEGALVsilNSOe211XhU+T4D0YsZICSwQnh4fBmODITncwKZ\nQuwX/wZIWw11pznwERVLuzXgFXuWNN0ypMusjxHJgXC4HAA4653Zx5BbE5QaGqvF\n6DVS8AU0iQKBgBlWiyHbkRnasOww6atHGrZzQNfeRu4tkw7bAJ8ZW+ht9DjnK5Ed\nBJkHMBze3IhiUu5LjUPJGdMinaFBjL0Ig9BJL3sUjJU9NGLsmVT7s2NrQcOANrdH\nW3T0/EoZkTpJRq4fueVffPOR4PW/WHmFN79OfdsszooxU3U3XRtEjRMxAoGBAJ8i\nDo342iKbO1WSoBJm41xekl+HmPb+B9VaJMg8GLTsEtCEVa9jIjuoACzx902mALxj\njekHu2CIY73bh4IivAFlB4mV6JK+cL1O/MXBGYS5o9UJiVm98ZdBMWog+olfjtnx\ne0IGcm693CQxZcBbLDPs5M1ArJBbFe3r3EkBY725AoGBAJynW1kvhqUZwPPLDDLP\nl0++7Hm4JJIyTsZNdnfcSHZCWFAd4MZDpONNda1KwTloX/ljmBgkImP+SiVCcIGn\nUvQA/eqCp8+t/2trS5XsPhPfHBzL9AHIi+3ffL8EqdMFg7Ld3LKyqAYANe6pRn1F\ncdJVD2cCb/Im7nIaHjRoQ3yk\n-----END PRIVATE KEY-----\n"",
                    ""client_email"": ""firebase-adminsdk-dlo79@dota-picking-partner.iam.gserviceaccount.com"",
                    ""client_id"": ""109323270064210222984"",
                    ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
                    ""token_uri"": ""https://oauth2.googleapis.com/token"",
                    ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
                    ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-dlo79%40dota-picking-partner.iam.gserviceaccount.com""
                }
"
        };
        public async Task<Dictionary<string, object>> GetHeroCounters(string hero)
        {
            //Initializing firebase
            FirestoreClient firestoreClient = builder.Build();
            FirestoreDb db = FirestoreDb.Create("dota-picking-partner", firestoreClient);
            //Gets the document from HeroCounterPicks collection with given hero name
            DocumentReference docRef = db.Collection("HeroCounterPicks").Document(hero);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            Dictionary<string, object> map = new Dictionary<string, object>();
            map = snapshot.ConvertTo<Dictionary<string, object>>();
            return map;
        }

        public async void PostUserFeedback(UserFeedback feedback)
        {
            //Initializing firebase
            FirestoreClient firestoreClient = builder.Build();
            FirestoreDb db = FirestoreDb.Create("dota-picking-partner", firestoreClient);
            DocumentReference docRef = db.Collection("UserFeedback").Document(feedback.ContactInfo);
            Dictionary<string, object> map = new Dictionary<string, object>();
            map[feedback.ContactInfo] = feedback.Comment;

            await docRef.SetAsync(map);
        }

        public async Task RemoveUserFeedback(string docName)
        {
            FirestoreClient firestoreClient = builder.Build();
            FirestoreDb db = FirestoreDb.Create("dota-picking-partner", firestoreClient);
            DocumentReference docRef = db.Collection("UserFeedback").Document(docName);

            await docRef.DeleteAsync();
        }

        public async Task<RecentFeedback> GetAllUserFeedback()
        {
            //Initializing firebase
            FirestoreClient firestoreClient = builder.Build();
            FirestoreDb db = FirestoreDb.Create("dota-picking-partner", firestoreClient);
            QuerySnapshot collection = await db.Collection("UserFeedback").GetSnapshotAsync();
            RecentFeedback feedback = new RecentFeedback();
            feedback.recentFeedback = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot document in collection.Documents)
            {
                Dictionary<string, object> data = document.ToDictionary();
                feedback.recentFeedback.Add(data);
            }

            return feedback;
        }

        public CombinedHeroCounters CombineHeroWinrates(HeroCounters heroCounters)
        {
            CombinedHeroCounters combinedHeroCounters = new CombinedHeroCounters();
            if (heroCounters.heroOneCounters is not null)
            {
                if (heroCounters.heroTwoCounters is not null)
                {
                    if (heroCounters.heroThreeCounters is not null)
                    {
                        if (heroCounters.heroFourCounters is not null)
                        {
                            if (heroCounters.heroFiveCounters is not null)
                            {
                                combinedHeroCounters.combinedHeroCounters = heroCounters.heroOneCounters
                                    .Zip(heroCounters.heroTwoCounters, (kvp1, kvp2) => new { kvp1.Key, Value = Convert.ToDouble(kvp1.Value) + Convert.ToDouble(kvp2.Value) })
                                    .Zip(heroCounters.heroThreeCounters, (kvp12, kvp3) => new { kvp12.Key, Value = (kvp12.Value + Convert.ToDouble(kvp3.Value)) })
                                    .Zip(heroCounters.heroFourCounters, (kvp123, kvp4) => new { kvp123.Key, Value = (kvp123.Value + Convert.ToDouble(kvp4.Value)) })
                                    .Zip(heroCounters.heroFiveCounters, (kvp1234, kvp5) => new { kvp1234.Key, Value = (object)Math.Round(((kvp1234.Value + Convert.ToDouble(kvp5.Value)) / 5.0), 2) })
                                    .ToDictionary(x => x.Key, x => x.Value);
                                goto done;
                            }
                            combinedHeroCounters.combinedHeroCounters = heroCounters.heroOneCounters
                                .Zip(heroCounters.heroTwoCounters, (kvp1, kvp2) => new { kvp1.Key, Value = Convert.ToDouble(kvp1.Value) + Convert.ToDouble(kvp2.Value) })
                                .Zip(heroCounters.heroThreeCounters, (kvp12, kvp3) => new { kvp12.Key, Value = (kvp12.Value + Convert.ToDouble(kvp3.Value)) })
                                .Zip(heroCounters.heroFourCounters, (kvp123, kvp4) => new { kvp123.Key, Value = (object)Math.Round(((kvp123.Value + Convert.ToDouble(kvp4.Value)) / 4.0), 2) })
                                .ToDictionary(x => x.Key, x => x.Value);
                            goto done;
                        }
                        combinedHeroCounters.combinedHeroCounters = heroCounters.heroOneCounters
                                .Zip(heroCounters.heroTwoCounters, (kvp1, kvp2) => new { kvp1.Key, Value = Convert.ToDouble(kvp1.Value) + Convert.ToDouble(kvp2.Value) })
                                .Zip(heroCounters.heroThreeCounters, (kvp12, kvp3) => new { kvp12.Key, Value = (object)Math.Round(((kvp12.Value + Convert.ToDouble(kvp3.Value)) / 3.0), 2) })
                                .ToDictionary(x => x.Key, x => x.Value);
                        goto done;
                    }
                    combinedHeroCounters.combinedHeroCounters = heroCounters.heroOneCounters
                                .Zip(heroCounters.heroTwoCounters, (kvp1, kvp2) => new { kvp1.Key, Value = (object)Math.Round(((Convert.ToDouble(kvp1.Value) + Convert.ToDouble(kvp2.Value)) / 2.0), 2) })
                                .ToDictionary(x => x.Key, x => x.Value);
                    goto done;
                }
                combinedHeroCounters.combinedHeroCounters = heroCounters.heroOneCounters;
            }


        done:
            return combinedHeroCounters;
        }

        public static List<ScraperData> GetCountersForHero(string heroName)
        {
            var url = $"https://www.dotabuff.com/heroes/{heroName}/counters";
            var useragent = "Mozilla/5.0 (Linux; Android 6.0.1; Nexus 5X Build/MMB29P) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/W.X.Y.Z Mobile Safari/537.36 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(useragent);

                var html = httpClient.GetStringAsync(url).Result;
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                List<ScraperData> counters = new List<ScraperData>();
                var counterNodes = htmlDocument.DocumentNode.Descendants("td").Where(node => node.GetAttributeValue("class", "").Equals("cell-xlarge")).ToList();

                foreach (var counterNode in counterNodes)
                {
                    counters.Add(new ScraperData
                    {
                        HeroName = counterNode?.Descendants("a")?.FirstOrDefault()?.InnerText,
                        Disadvantage = counterNode?.ParentNode?.Descendants("td")?.ElementAt(2)?.GetAttributeValue("data-value", ""),
                        WinRate = counterNode?.ParentNode?.Descendants("td")?.ElementAt(3)?.GetAttributeValue("data-value", ""),
                        MatchesPlayed = counterNode?.ParentNode?.Descendants("td")?.ElementAt(4)?.GetAttributeValue("data-value", "")
                    });
                }

                return counters;
            }
        }

        public async Task RunScraper()
        {
            //Full array of all hero names in Dota2 written for URLS
            string[] heroNames = new string[] {
            "abaddon", "alchemist", "ancient-apparition", "anti-mage", "arc-warden", "axe", "bane", "batrider", "beastmaster",
            "bloodseeker", "bounty-hunter", "brewmaster", "bristleback", "broodmother", "centaur-warrunner", "chaos-knight", "chen",
            "clinkz", "clockwerk", "crystal-maiden", "dark-seer", "dark-willow", "dazzle", "death-prophet", "disruptor",
            "doom", "dragon-knight", "drow-ranger", "earth-spirit", "earthshaker", "elder-titan", "ember-spirit", "enchantress",
            "enigma", "faceless-void", "grimstroke", "gyrocopter", "huskar", "invoker", "io", "jakiro", "juggernaut",
            "keeper-of-the-light", "kunkka", "legion-commander", "leshrac", "lich", "lifestealer", "lina", "lion", "lone-druid",
            "luna", "lycan", "magnus", "mars", "medusa", "meepo", "mirana", "monkey-king", "morphling","muerta", "naga-siren",
            "natures-prophet", "necrophos", "night-stalker", "nyx-assassin", "ogre-magi", "omniknight", "oracle", "outworld-destroyer",
            "pangolier", "phantom-assassin", "phantom-lancer", "phoenix", "puck", "pudge", "pugna", "queen-of-pain", "razor",
            "riki", "rubick", "sand-king", "shadow-demon", "shadow-fiend", "shadow-shaman", "silencer", "skywrath-mage", "slardar",
            "slark", "snapfire", "sniper", "spectre", "spirit-breaker", "storm-spirit", "sven", "techies", "templar-assassin",
            "terrorblade", "tidehunter", "timbersaw", "tinker", "tiny", "treant-protector", "troll-warlord", "tusk", "underlord",
            "undying", "ursa", "vengeful-spirit", "venomancer", "viper", "visage", "void-spirit", "warlock", "weaver", "windranger",
            "winter-wyvern", "witch-doctor", "wraith-king", "zeus"
            };
            string[] heroDocNames = new string[] {
            "abaddon", "alchemist", "ancient_apparition", "anti_mage", "arc_warden", "axe", "bane", "batrider", "beastmaster",
            "bloodseeker", "bounty_hunter", "brewmaster", "bristleback", "broodmother", "centaur_warrunner", "chaos_knight", "chen",
            "clinkz", "clockwerk", "crystal_maiden", "dark_seer", "dark_willow", "dazzle", "death_prophet", "disruptor",
            "doom", "dragon_knight", "drow_ranger", "earth_spirit", "earthshaker", "elder_titan", "ember_spirit", "enchantress",
            "enigma", "faceless_void", "grimstroke", "gyrocopter", "huskar", "invoker", "io", "jakiro", "juggernaut",
            "keeper_of_the_light", "kunkka", "legion_commander", "leshrac", "lich", "lifestealer", "lina", "lion", "lone_druid",
            "luna", "lycan", "magnus", "mars", "medusa", "meepo", "mirana", "monkey_king", "morphling","muerta", "naga_siren",
            "natures_prophet", "necrophos", "night_stalker", "nyx_assassin", "ogre_magi", "omniknight", "oracle", "outworld_destroyer",
            "pangolier", "phantom_assassin", "phantom_lancer", "phoenix", "puck", "pudge", "pugna", "queen_of_pain", "razor",
            "riki", "rubick", "sand_king", "shadow_demon", "shadow_fiend", "shadow_shaman", "silencer", "skywrath_mage", "slardar",
            "slark", "snapfire", "sniper", "spectre", "spirit_breaker", "storm_spirit", "sven", "techies", "templar_assassin",
            "terrorblade", "tidehunter", "timbersaw", "tinker", "tiny", "treant_protector", "troll_warlord", "tusk", "underlord",
            "undying", "ursa", "vengeful_spirit", "venomancer", "viper", "visage", "void_spirit", "warlock", "weaver", "windranger",
            "winter_wyvern", "witch_doctor", "wraith_king", "zeus"
            };
            FirestoreClient firestoreClient = builder.Build();
            FirestoreDb db = FirestoreDb.Create("dota-picking-partner", firestoreClient);
            int i = 0;
            foreach (var hero in heroNames)
            {
                var docName = heroDocNames[i];
                var counters = GetCountersForHero(hero);
                DocumentReference docRef = db.Collection("HeroCounterPicks").Document(docName);
                Dictionary<string, object> map = new Dictionary<string, object>();
                foreach (var counter in counters)
                {
                    var counterWinrate = Convert.ToDouble(counter.WinRate);
                    var winRate = Math.Round((100.0 - counterWinrate), 2);
                    map[counter.HeroName] = winRate;
                }
                await docRef.SetAsync(map);
                i++;
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }

            return;
        }
    }
    
}

