using System;
using System.Linq;
using System.Collections.Generic;


namespace MultiValueDictionary.Runner
{
    public class DataStoreDriver
    {
        private Dictionary<string, List<string>> _kvpList;

        public DataStoreDriver()
        {
            _kvpList = new Dictionary<string, List<string>>();
        }

        public DataStoreDriver(Dictionary<string, List<string>> kvpList)
        {
            _kvpList = kvpList;
        }

        public List<string> GetKeys()
        {
            return _kvpList.Keys.ToList();
        }

        public List<string> GetMembers(string key)
        {
            if(!_kvpList.ContainsKey(key))
            {
                throw new KeyNotFoundException("ERROR: key does not exist");
            }

            return _kvpList[key];
        }

        public void Add(string key, string value)
        {
            if(!_kvpList.ContainsKey(key))
            {
                _kvpList.Add(key, new List<string>(){value});
            } 
            else if(!_kvpList[key].Contains(value))
            {
                _kvpList[key].Add(value);
            }
            else 
            {
                throw new DuplicateMemberException("ERROR: member already exists for key");
            }
        }
        
        public void Remove(string key, string value)
        {
            if(!_kvpList.ContainsKey(key))
            {
                throw new KeyNotFoundException("ERROR: key does not exist");
            }
            else if(!_kvpList[key].Contains(value))
            {
                throw new MemberNotFoundException("ERROR: member does not exist");
            }
            else if(_kvpList[key].Count > 1)
            {
                _kvpList[key].Remove(value);
            }
            else
            {
                _kvpList.Remove(key);
            }
        }

        public void RemoveAll(string key)
        {
            if(_kvpList.ContainsKey(key))
            {
                _kvpList.Remove(key);
            }
            else
            {
                throw new KeyNotFoundException("ERROR: key does not exist");
            }
        }

        public void Clear()
        {
            _kvpList.Clear();
        }

        public bool DoesKeyExist(string key)
        {
            return _kvpList.ContainsKey(key);
        }

        public bool DoesMemberExist(string key, string value)
        {
            if(!_kvpList.ContainsKey(key))
            {
                throw new KeyNotFoundException("ERROR: key does not exist");
            } 
            else {
                return _kvpList[key].Contains(value);
            }
        }

        public List<string> GetAllMembers()
        {
            List<string> memberList = _kvpList.Aggregate(new List<string>(), (acc, kvp) =>  acc.Concat(kvp.Value).ToList());

            return memberList;
        }

        public Dictionary<string, List<string>> GetItems()
        {
            return _kvpList;
        }
    }
}