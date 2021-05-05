using System.Collections.Generic;
using FullSerializer;
using Proyecto26;

public static class DataBaseHandler
{
    private const string projectId = "testing-312216";
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.europe-west1.firebasedatabase.app/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostUserCallback();
    public delegate void PostGameCallback();
    public delegate void PostInventoryCallback();
    public delegate void GetUserCallback(User user);
    public delegate void GetGamesCallback(Dictionary<string, Game> games);
    public delegate void GetInventoryCallback(InventoryData inventory);
    public delegate void GetUsersCallback(Dictionary<string, User> users);


    /*
     * Adds a user to the Firebase Database
     *
     * @param user  User object that will be uploaded
     * @param userId Id of the user that will be uploaded
     * @param callback What to do after the user is uploaded successfully
    */
    public static void PostUser(User user, string userId, PostUserCallback callback, string idToken)
    {
        RestClient.Put<User>($"{databaseURL}users/{userId}.json?auth={idToken}", user).Then(response => { callback(); });

    }

    /*
    * Adds a game for an specific user to the Firebase Database
    *
    * @param game  Game object that will be uploaded
    * @param userId Id of the user that will be uploaded
    * @param callback What to do after the game is uploaded successfully
    * @param idToken string necessary for authentication
   */
    public static void PostGame(Game game, string userId, PostGameCallback callback, string idToken)
    {
        RestClient.Post<Game>($"{databaseURL}games/{userId}.json?auth={idToken}", game).Then(response => { callback(); });
    }

    /*
    * Adds an inventory for an specific user to the Firebase Database
    *
    * @param inventory  Inventory object that will be uploaded
    * @param userId Id of the user that will be uploaded
    * @param callback What to do after the game is uploaded successfully
    * @param idToken string necessary for authentication
   */
    public static void PostInventory(InventoryData inventory, string userId, PostInventoryCallback callback, string idToken)
    {
        RestClient.Put<InventoryData>($"{databaseURL}inventory/{userId}.json?auth={idToken}", inventory).Then(response => { callback(); });
    }



    /*
     * Retrieves a user from the Firebase Database, given their id
     *
     * @param userId Id of the user that we are looking for
     * @param callback What to do after the user is downloaded successfully
     * @param idToken string necessary for authentication
    */
    public static void GetUser(string userId, GetUserCallback callback, string idToken)
    {
        RestClient.Get<User>($"{databaseURL}users/{userId}.json?auth={idToken}").Then(user => { callback(user); });

    }

    /*
     * Retrieves all the games of an specific user from the Firebase Database, given the user's id
     *
     * @param userId Id of the user of which we want to get the games
     * @param idToken string necessary for authentication
     * @param callback What to do after the games are downloaded successfully
    */
    public static void GetGames(string userId, string idToken, GetGamesCallback callback)
    {
        //RestClient.Get<GameSession>($"{databaseURL}games/{userId}.json?auth={idToken}").Then(game => { callback(game); });
        RestClient.Get($"{databaseURL}games/{userId}.json?auth={idToken}").Then(
        response =>
            {
                var responseJson = response.Text;

                // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
                // to serialize more complex types (a Dictionary, in this case)
                var data = fsJsonParser.Parse(responseJson);
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, Game>), ref deserialized);

                var games = deserialized as Dictionary<string, Game>;
                callback(games);
            });
    }


    /*
    * Retrieves the inventory from a certain user from the Firebase Database, given their id
    *
    * @param userId Id of the user that we are looking for
    * @param callback What to do after the user is downloaded successfully
    * @param idToken string necessary for authentication
   */
    public static void GetInventory(string userId, GetInventoryCallback callback, string idToken)
    {
        RestClient.Get<InventoryData>($"{databaseURL}inventory/{userId}.json?auth={idToken}").Then(inventory => { callback(inventory); });

    }

    /// <summary>
    /// Gets all users from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    ///

    public static void GetUsers(GetUsersCallback callback, string idToken)
    {
        RestClient.Get($"{databaseURL}users.json?auth={idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);

            var users = deserialized as Dictionary<string, User>;
            callback(users);
        });
    }


}
