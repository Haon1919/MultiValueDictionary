using System;
using System.Linq;
using System.Collections.Generic;


namespace MultiValueDictionary.Runner
{
    public class InputDriver
    {
        private DataStoreDriver dsd = new DataStoreDriver();

        public bool HandleInput(string input)
        {
            string[] inputComponents = input.Split(' ');
            var command = inputComponents[0];

            switch(command)
            {
                case "ADD":
                    FormatAddResponse(inputComponents);
                    break;
                case "REMOVE":
                    FormatRemoveResponse(inputComponents);
                    break;
                case "REMOVEALL":
                    FormatRemoveAllResponse(inputComponents);
                    break;
                case "KEYS":
                    FormatKeysResponse();
                    break;
                case "MEMBERS":
                    FormatMembersResponse(inputComponents);
                    break;
                case "CLEAR":
                    FormatClearResponse();
                    break;
                case "KEYEXISTS":
                    FormatKeyExistsResponse(inputComponents);
                    break;
                case "MEMBEREXISTS":
                    FormatMemberExistsResponse(inputComponents);
                    break;
                case "ALLMEMBERS":
                    FormatAllMembersResponse();
                    break;
                case "ITEMS":
                    FormatItemsResponse();
                    break;
                case "HELP":
                    FormatHelpResponse();
                    break;
                case "Q":
                    return false;
                
                default:
                    Console.WriteLine("Invalid command: Please use valid command. For a full list try running the HELP command.");
                    break;
            }

            return true;
        }

        private void FormatAddResponse(string[] argumentList)
        {
            try 
            {
                string key = argumentList[1];
                string member = argumentList[2];

                dsd.Add(key, member);
                Console.WriteLine(") Added");
            }
            catch(DuplicateMemberException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch(IndexOutOfRangeException exc)
            {
                Console.WriteLine("ERROR: missing required paramaters (ADD <key> <member>)");
            }
        }

        private void FormatKeysResponse()
        {
            List<String> keys = dsd.GetKeys();
            
            for(int i = 0; i < keys.Count; i++)
            {
                Console.WriteLine($"{i+1}) {keys[i]}");
            }
        }

        
        private void FormatMembersResponse(string[] argumentList)
        {
            try 
            {
                string key = argumentList[1];
                List<string> members = dsd.GetMembers(key);

                for(int i = 0; i < members.Count; i++)
                {
                    Console.WriteLine($"{i+1}) {members[i]}");
                }

            }
            catch(KeyNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        
        private void FormatRemoveResponse(string[] argumentList)
        {
            try 
            {
                string key = argumentList[1];
                string member = argumentList[2];
                dsd.Remove(key, member);
                Console.WriteLine(") Removed");
            }
            catch(MemberNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch(KeyNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch(IndexOutOfRangeException exc)
            {
                Console.WriteLine("ERROR: missing required paramaters (REMOVE <key> <member>)");
            }
        }

        
        private void FormatRemoveAllResponse(string[] argumentList)
        {
            try 
            {
                var key = argumentList[1];
                dsd.RemoveAll(key);
                Console.WriteLine(") Removed");
            }
            catch(MemberNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch(KeyNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        
        private void FormatClearResponse()
        {
            dsd.Clear();
            Console.WriteLine(") Cleared");
        }

        private void FormatKeyExistsResponse(string[] argumentList)
        {
            string key = argumentList[1];
            bool keyExists = dsd.DoesKeyExist(key);
            Console.WriteLine(keyExists.ToString());
        }

        private void FormatMemberExistsResponse(string[] argumentList)
        {
            try
            {
                string key = argumentList[1];
                string member = argumentList[2];
                bool memberExists = dsd.DoesMemberExist(key, member);
                Console.WriteLine(memberExists.ToString());
            }
            catch(KeyNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private void FormatAllMembersResponse()
        {
            List<string> members = dsd.GetAllMembers();

            for(int i = 0; i < members.Count; i++)
            {
                Console.WriteLine($"{i+1}) {members[i]}");
            }
        }

        private void FormatItemsResponse()
        {
            Dictionary<string, List<string>> items = dsd.GetItems();

            foreach(var kvp in items)
            {
                Console.WriteLine($"{kvp.Key}: {String.Join(", ", kvp.Value)}");
            }
        }

        private void FormatHelpResponse()
        {
            Console.WriteLine("Commands:\n ADD,\n REMOVE,\n REMOVEALL,\n ITEMS,\n ALLMEMBERS,\n MEMBEREXISTS,\n KEYEXISTS,\n CLEAR,\n KEYS,\n MEMBERS");
        }
    }
}