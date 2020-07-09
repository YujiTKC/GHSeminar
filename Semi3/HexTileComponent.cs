using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Semi3
{
    public class HexTileComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the HexTileComponent class.
        /// </summary>
        public HexTileComponent()
          : base("HexTileComponent", "HT",
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
            pManager.AddNumberParameter("d", "d", "d", GH_ParamAccess.item,100.0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("pts", "pts", "points", GH_ParamAccess.list);
            pManager.AddCurveParameter("crv", "crv", "curves", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var nx = 10;
            var ny = 10;
            var d = 100.0;
            DA.GetData("nx", ref nx);
            DA.GetData("ny", ref ny);
            DA.GetData("d", ref d);

            // d
            var dd = 0.50 * d * Math.Sqrt(3.0);
            var dx = 0.50 * d + d;
            var dy = dd * 2;

            List<Point3d> pts = new List<Point3d>();

            for(int iy = 0; iy < ny; iy++)
            {
                for (int ix = 0; ix < nx; ix++)
                {
                    double x = dx * ix;
                    double y = dy * iy;
                    if (ix % 2 != 0)
                    {
                        y += dd;
                    }

                    var p = new Point3d(x, y, 0.0);
                    pts.Add(p);

                }
            }
            DA.SetDataList("pts", pts);


            List<Curve> crvs = new List<Curve>();
            foreach(Point3d p in pts)
            {
                Point3d[] hexPts = new Point3d[6];
                for(int i = 0; i<6; i++)
                {
                    double rad = 2.0 * Math.PI * i / 6.0;
                    double x = p.X + d * Math.Cos(rad);
                    double y = p.Y + d * Math.Sin(rad);
                    hexPts[i] = new Point3d(x, y, 0);

                }

                for(int i =0; i<5; i++)
                {
                    var line = new LineCurve(hexPts[i], hexPts[i + 1]);
                    crvs.Add(line);
                }
                crvs.Add(new LineCurve(hexPts[5], hexPts[0]));

            }


            DA.SetDataList("crv", crvs);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2a2dc479-545b-46fc-b035-ff4c6a715be7"); }
        }
    }
}