﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateLibrary.Strategy
{
    public interface IStrategy
    {
        void CompileCode(String templateCode, TextWriter output, 
            String[] namespaces, params Variable[] parameters);
    }
}
