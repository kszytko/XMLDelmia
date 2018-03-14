using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INFITF;
using DNBIgpOlp;
using DNBRobot;
using DNBIgpTagPath;
using PPR;
using ProductStructureTypeLib;

namespace XMLDelmia
{
    class DelmiaConnect
    {
        static DelmiaConnect singleton;

        INFITF.Application DelmiaInstance;
        INFITF.Document actualDocument;
        INFITF.Document oProcessDoc;

        PPR.PPRDocument processDoc;
        PPR.PPRProducts resources;

        Product orsCell;
        Product orsTagList;

        public static void Start()
        {
            if (singleton == null)
                singleton = new DelmiaConnect();

            singleton.ConnectToDelmia();
        }


        public void ConnectToDelmia()
        {
            DelmiaInstance = (INFITF.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("DELMIA.Application");
            actualDocument = DelmiaInstance.ActiveDocument;


            Selection sel = actualDocument.Selection;


            processDoc = (PPRDocument)actualDocument.GetItem("PPRDocument");
            resources = processDoc.Resources;
        }


        private void LoadRobotTask()
        {
            Object[] objRobotTasks = new Object[100];
            String folderName = "C:\\tmp\\test\\";


            Selection sel = actualDocument.Selection;

            Product robot = (Product)sel.Item(1).Value;

            RobGenericController objCtrl = (RobGenericController)robot.GetTechnologicalObject("RobGenericController");
            RobControllerFactory objFact = (RobControllerFactory)robot.GetTechnologicalObject("RobControllerFactory");
            RobotTaskFactory objRobotTaskFactory = (RobotTaskFactory)robot.GetTechnologicalObject("RobotTaskFactory");
            OLPTranslator olpAPI = (OLPTranslator)robot.GetTechnologicalObject("OLPTranslator");

            objRobotTaskFactory.GetAllRobotTasks(objRobotTasks);


            foreach (RobotTask robotTask in objRobotTasks)
            {
                olpAPI.DownloadAsXML(robotTask, folderName + robotTask.get_Name() + ".xml", false, false, false);
            }
        }

    }
}
