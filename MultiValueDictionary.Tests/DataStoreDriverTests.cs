using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System;
using MultiValueDictionary.Runner;

namespace MultiValueDictionary.Tests
{
    public class DataStoreDriverTests
    {
        [Fact]
        public void GetKeysReturnsListOfKeys()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1"}}
            };
            var ds = new DataStoreDriver(populatedDs);
            List<string> keyList = ds.GetKeys();

            Assert.Equal("Key1", keyList[0]);
        }

        [Fact]
        public void GetKeysOnEmptyDataStoreReturnsEmptyList()
        {
            var ds = new DataStoreDriver();
            List<string> keyList = ds.GetKeys();
            
            Assert.True(keyList.Count == 0);
        }

        [Fact]
        public void GetMembersReturnsListOfValuesForGivenKey()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1", "Member2", "Member3"}}
            };
            var ds = new DataStoreDriver(populatedDs);
            List<string> memberList = ds.GetMembers("Key1");
            
            Assert.True(memberList.Count == 3);

        }

        [Fact]
        public void GetMembersWithNonExistentKeyReturnsError()
        {
            var ds = new DataStoreDriver();
            Assert.Throws<KeyNotFoundException>(() => ds.GetMembers("NonExistentKey"));
        }

        [Fact]
        public void AddStoresNewKeyMemberPair()
        {
            var populatedDs = new Dictionary<string, List<string>>();
            var ds = new DataStoreDriver(populatedDs);
            ds.Add("Key1", "Member1");

            Assert.True(populatedDs["Key1"][0] == "Member1");
        }

        [Fact]
        public void AddingToExistingKeyStoresValueUnderSpecifiedKey()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1"}}
            };
            var ds = new DataStoreDriver(populatedDs);          
            ds.Add("Key1", "Member2");

            Assert.True(populatedDs["Key1"].Contains("Member2"));

        }

        [Fact]
        public void AddingDuplicateMemberToKeyReturnsError()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1"}}
            };
            var ds = new DataStoreDriver(populatedDs);

            Assert.Throws<DuplicateMemberException>(() => ds.Add("Key1", "Member1"));
        }

        [Fact]
        public void RemoveDeletesMemberForGivenKey()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1", "Member2", "Member3"}}
            };
            var ds = new DataStoreDriver(populatedDs);
            ds.Remove("Key1", "Member1");

            Assert.False(populatedDs["Key1"].Contains("Member1"));
        }

        [Fact]
        public void RemoveOnKeyWithOneMemberRemovesKeyMemberPair()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1"}}
            };
            var ds = new DataStoreDriver(populatedDs);
            ds.Remove("Key1", "Member1");
            Assert.False(populatedDs.ContainsKey("Key1"));
        }

        [Fact]
        public void RemoveOnNonExistentKeyReturnsError()
        {
            var ds = new DataStoreDriver();
            Assert.Throws<KeyNotFoundException>(() => ds.Remove("Key1", "Member1"));
        }

        [Fact]
        public void RemoveOnKeyWithoutSpecifiedMemberReturnsError()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1"}}
            };
            var ds = new DataStoreDriver(populatedDs);

            Assert.Throws<MemberNotFoundException>(() => ds.Remove("Key1", "Member2"));
        }
        
        [Fact]
        public void RemoveAllDeletesKeyMemberPair()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1", "Member2", "Member3"}}
            };
            var ds = new DataStoreDriver(populatedDs);
            ds.RemoveAll("Key1");

            Assert.False(populatedDs.ContainsKey("Key1"));
        }

        [Fact]
        public void RemoveAllOnNonExistentKeyReturnsError()
        {
            var ds = new DataStoreDriver();
            Assert.Throws<KeyNotFoundException>(() => ds.RemoveAll("Key1"));
        }

        [Fact]
        public void ClearRemovesAllKeyMemberPairs()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1", "Member2", "Member3"}},
                {"Key2", new List<string>() {"Member1", "Member2", "Member3"}}
            };
            var ds = new DataStoreDriver(populatedDs);
            ds.Clear();

            Assert.True(populatedDs.Count == 0);
        }

        [Fact]
        public void ClearOnEmptyDataStoreReturnsEmptyList()
        {
            var populatedDs = new Dictionary<string, List<string>>();
            var ds = new DataStoreDriver(populatedDs);
            ds.Clear();

            Assert.True(populatedDs.Count == 0);
        }
        
        [Fact]
        public void DoesMemberExistReturnsTrueForExistingKeyMember()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1"}},
            };
            var ds = new DataStoreDriver(populatedDs);
            bool response = ds.DoesMemberExist("Key1", "Member1");

            Assert.True(response);
        }

        [Fact]
        public void DoesMemberExistReturnsFalseForNonExistentKeyMember()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1"}},
            };
            var ds = new DataStoreDriver(populatedDs);            
            bool response = ds.DoesMemberExist("Key1", "Member2");

            Assert.False(response);
        }

        [Fact]
        public void DoesMemberExistOnNonExistentKeyReturnsError()
        {
            var ds = new DataStoreDriver();
            Assert.Throws<KeyNotFoundException>(() => ds.DoesMemberExist("Key1", "Member2"));
        }

        [Fact]
        public void DoesKeyExistReturnsTrueForExistentKey()
        {
           var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1"}},
            };
            var ds = new DataStoreDriver(populatedDs);
            bool response = ds.DoesKeyExist("Key1");

            Assert.True(response);
        }

        [Fact]
        public void DoesKeyExistReturnsFalseForNonExistentKey()
        {
            var populatedDs = new Dictionary<string, List<string>>();
            var ds = new DataStoreDriver(populatedDs);
            bool response = ds.DoesKeyExist("Key1");

            Assert.False(response);
        }

        [Fact]
        public void GetAllMembersReturnsAllExistingMembers()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1", "Member2", "Member3"}},
                {"Key2", new List<string>() {"Member1", "Member2", "Member3"}}
            };
            var ds = new DataStoreDriver(populatedDs);
            List<string> memberList = ds.GetAllMembers();

            var memberCount = populatedDs.Values.ToList().Aggregate(0, (total, current) => total + current.Count());
            var expectedMemberCount = memberList.Count();

            Assert.Equal(memberCount, expectedMemberCount);
        }

        [Fact]
        public void GetAllMembersOnEmptyDataStoreReturnsEmptyList()
        {
            var populatedDs = new Dictionary<string, List<string>>();
            var ds = new DataStoreDriver(populatedDs);
            List<string> memberList = ds.GetAllMembers();

            Assert.True(memberList.Count() == 0);

        }

        [Fact]
        public void GetItemsReturnsAllExistingKeyMemberPairs()
        {
            var populatedDs = new Dictionary<string, List<string>>()
            {
                {"Key1", new List<string>() {"Member1", "Member2", "Member3"}},
                {"Key2", new List<string>() {"Member1", "Member2", "Member3"}}
            };
            var ds = new DataStoreDriver(populatedDs);
            Dictionary<string, List<string>> itemList = ds.GetItems();

            Dictionary<string, List<string>> uniqueKeys = itemList.Where(entry => !populatedDs.ContainsKey(entry.Key))
            .ToDictionary(entry => entry.Key, entry => entry.Value);

            Dictionary<string, List<string>> uniqueValues = itemList.Where(entry => !populatedDs[entry.Key].SequenceEqual(itemList[entry.Key]))
            .ToDictionary(entry => entry.Key, entry => entry.Value);

            Assert.True(uniqueKeys.Count + uniqueValues.Count == 0);
        }

        [Fact]
        public void GetItemsOnEmptyDataStoreReturnsEmptyList()
        {
            var populatedDs = new Dictionary<string, List<string>>();
            var ds = new DataStoreDriver(populatedDs);
            Dictionary<string, List<string>> itemList = ds.GetItems();

            Assert.True(itemList.Count == 0);
        }
    }
}
