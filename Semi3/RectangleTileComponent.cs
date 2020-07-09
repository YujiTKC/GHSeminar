using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace Semi3
{
    public class RectangleTileComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public RectangleTileComponent()
          : base("RecTile", "RT",
              "Description",
              "GHSemi", "tile")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("nx", "nx", "nx", GH_ParamAccess.item,10);
            pManager.AddIntegerParameter("ny", "ny", "ny", GH_ParamAccess.item,10);
            pManager.AddNumberParameter("dx", "dx", "dx", GH_ParamAccess.item,100);
            pManager.AddNumberParameter("dy", "dy", "dy", GH_ParamAccess.item,100);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("crv", "c", "curves", GH_ParamAccess.list);
            pManager.AddPointParameter("pts", "p", "points", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var nx = 10;
            var ny = 10;
            var dx = 100.0;
            var dy = 100.0;
            DA.GetData("nx", ref nx);
            DA.GetData("ny", ref ny);
            DA.GetData("dx", ref dx);
            DA.GetData("dy", ref dy);


            List<Point3d> pts = new List<Point3d>();
            List<Curve> crvs = new List<Curve>();

            for (int iy = 0; iy < ny; iy++)
            {
                for(int ix = 0; ix < nx; ix++)
                {
                    Point3d p = new Point3d(ix * dx, iy * dy, 0.0);
                    pts.Add(p);

                    var p1 = new Point3d(p.X - dx * 0.5, p.Y - dy * 0.5, 0.0);
                    var p2 = new Point3d(p.X + dx * 0.5, p.Y - dy * 0.5, 0.0);
                    var p3 = new Point3d(p.X + dx * 0.5, p.Y + dy * 0.5, 0.0);
                    var p4 = new Point3d(p.X - dx * 0.5, p.Y + dy * 0.5, 0.0);

                    var l1 = new LineCurve(p1, p2);
                    var l2 = new LineCurve(p2, p3);
                    var l3 = new LineCurve(p3, p4);
                    var l4 = new LineCurve(p4, p1);

                    crvs.Add(l1);
                    crvs.Add(l2);
                    crvs.Add(l3);
                    crvs.Add(l4);

                }
            }

            DA.SetDataList("pts", pts);
            DA.SetDataList("crv", crvs);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("04f4796f-0bbe-4842-9ac9-899fa4cae332"); }
        }
    }
}
