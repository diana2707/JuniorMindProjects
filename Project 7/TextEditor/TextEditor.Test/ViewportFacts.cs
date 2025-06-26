using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class ViewportFacts
    {
        [Fact]
        public void ViewportObjectExists()
        {
            Viewport viewport = new(120, 30, 0);
            Assert.IsType<Viewport>(viewport);
        }
    }
}
