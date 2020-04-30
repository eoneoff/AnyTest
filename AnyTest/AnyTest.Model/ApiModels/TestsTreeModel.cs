using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.Model.ApiModels
{
    public class TestsTreeModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public IEnumerable<TestsTreeModel> Children { get; set; }
    }
}
