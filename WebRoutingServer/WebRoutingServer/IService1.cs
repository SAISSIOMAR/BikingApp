using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebRoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ajoutez vos opérations de service ici
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "WebRoutingServer.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class Feature
    {
        [DataMember]
        public Properties properties { get; set; }

        [DataMember]

        public Geometry geometry { get; set; }
        [DataMember]

        public double[] bbox { get; set; }

        public Feature()
        {
        }
    }
    [DataContract]
    public class Geometry
    {
        [DataMember]

        public List<List<double>> coordinates { get; set; }
        [DataMember]

        public string type { get; set; }

        public Geometry() { }
    }
    [DataContract]
    public class Itinerary
    {
        [DataMember]
        public Feature[] features { get; set; }

        [DataMember]
        public double[] bbox { get; set; }
        public Itinerary()
        {

        }
    }
    [DataContract]
    public class Properties
    {
        [DataMember]
        public Segment[] segments { get; set; }
        public Properties() { }


    }
    [DataContract]

    public class Segment
    {
        [DataMember]

        public double distance { get; set; }
        [DataMember]

        public double duration { get; set; }
        [DataMember]

        public Step[] steps { get; set; }
        public Segment()
        {

        }
    }
    [DataContract]

    public class Step
    {
        [DataMember]

        public double distance { get; set; }
        [DataMember]

        public double duration { get; set; }
        [DataMember]

        public int type { get; set; }
        [DataMember]

        public string instruction { get; set; }
        [DataMember]

        public string name { get; set; }

        public Step() { }


    }
}
