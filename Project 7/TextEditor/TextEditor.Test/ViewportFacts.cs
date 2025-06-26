using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class ViewportFacts
    {
        [Fact]
        public void ViewportObjectExists()
        {
            Viewport viewport = new();
            Assert.IsType<Viewport>(viewport);
        }
    }
}
