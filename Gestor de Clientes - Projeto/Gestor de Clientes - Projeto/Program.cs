using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Clientes___Projeto
{
    internal class Program
    {

        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string Cpf;
        }
        
        static List <Cliente> clientes = new List <Cliente> ();

        enum Menu { Listagem = 1, Adicionar =2 , Remover = 3, Sair = 4}
        
        static void Main(string[] args)
        {
            carregar();
            bool escolheuSair = false;

            while (!escolheuSair) 
            {
                Console.WriteLine("Sitema de crientes - Bem vindo !");
                Console.WriteLine("1-Listagem\n2-adiconar\n3-remover\n4-sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }
    
          static void Adicionar() 
          { 
            Cliente cliente = new Cliente ();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente");
            cliente.Cpf = Console.ReadLine();
           
            clientes.Add (cliente);
            Salvar();
            Console.WriteLine("Cadastro concluido, aperte ENTER para sair !");
            Console.ReadLine();    
        }    
        
        static void Listagem() 
        {

            if (clientes.Count > 0) 
            {
                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome :{cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.Cpf}");
                    Console.WriteLine("=====================");
                    i++;               
                }
            }
            else
            { 
                Console.WriteLine("Nehum cliente cadastrado !");
            
            }
     
             Console.WriteLine("Aperte ENTER para sair .");
             Console.ReadLine ();
        
        }
         
         static void Salvar()
        {
            FileStream stream = new FileStream ("Cliente.dat",FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        } 
        
        static void Remover() 
        {
            Listagem();
            Console.WriteLine("Digite o ID do clinete que deseja remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 & id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();


            }
            else 
            {
                Console.WriteLine("id digitado é invalido, tente novamente !");
                Console.ReadLine();
            }
        }


        static void carregar() 
        {
            FileStream stream = new FileStream("Cliente.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if (clientes == null )
                {
                    clientes =new List<Cliente>();  

                }
                stream.Close();

            }
            catch (Exception ex) 
            { 
                 clientes = new List <Cliente>();

            }
            
            stream.Close ();
            
        }
    }
}
