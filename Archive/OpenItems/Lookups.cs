using System.Collections.Generic;
using System.Linq;
using OpenItems.Data;

namespace GSA.OpenItems.Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Configuration;
    using System.Data.SqlClient;
    using Data;

    /// <summary>
    /// Summary description for Lookups
    /// </summary>
    public class Lookups
    {
        private readonly ILookupDataLayer Dal;
        private readonly AdminBO Admin;
        public Lookups(ILookupDataLayer dal, AdminBO admin)
        {
            Dal = dal;
            Admin = admin;
        }

        public List<spGetDataSourceTypes_Result> GetDataSourceTypes()
        {
            var _page = new PageBase();
            if (_page.DataSourceTypes == null)
            {
                var dataSourceTypes = Dal.GetDataSourceTypes().ToList();
                ApplicationAssert.CheckCondition(dataSourceTypes.Any(), "There is the problem to load application configurations. Please contact your system administrator.");
                _page.DataSourceTypes = dataSourceTypes;
            }
            return _page.DataSourceTypes;

        }

        public List<spGetOpenItemsTypes_Result> GetOpenItemTypes()
        {
            var _page = new PageBase();
            if (_page.OpenItemsTypes == null)
            {
                var openItemsTypes = Dal.GetOpenItemsTypes().ToList();
                ApplicationAssert.CheckCondition(openItemsTypes.Any(), "There is the problem to load application configurations. Please contact your system administrator.");
                _page.OpenItemsTypes = openItemsTypes;
            }
            return _page.OpenItemsTypes;
        }

        public DataTable GetBA53AccrualTypes()
        {
            var _page = new PageBase();
            if (_page.BA53AccrualTypes == null)
            {
                var ds = Dal.GetBA53AccrualTypes().ToList().ToDataSet();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application configurations. Please contact your system administrator.");
                _page.BA53AccrualTypes = ds.Tables[0];
            }
            _page.BA53AccrualTypes.Columns["AccrualTypeCode"].ReadOnly = true;//hidden column
            _page.BA53AccrualTypes.Columns["AccrualTypeLabel"].ReadOnly = true;//hidden column
            _page.BA53AccrualTypes.Columns["LastUpdate"].ReadOnly = true;//hidden column
            return _page.BA53AccrualTypes;
        }

        public DataTable GetBA53AccrualTypeActions(int iAccrualTypeCode)
        {
            var _page = new PageBase();

            var ds = Dal.GetBA53AccrualTypeActions(iAccrualTypeCode).ToList().ToDataSet();
            ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application configurations. Please contact your system administrator.");
            _page.BA53AccrualTypeActions = ds.Tables[0];

            _page.BA53AccrualTypeActions.Columns["AccrualTypeActionCode"].ReadOnly = true;//hidden column
            _page.BA53AccrualTypeActions.Columns["AccrualTypeCode"].ReadOnly = true;//hidden column
            return _page.BA53AccrualTypeActions;
        }

        public DataSet GetLoadList()
        {
            var _page = new PageBase();
            if (_page.LoadList == null)
            {
                var ds = Dal.GetLoadList().ToList().ToDataSet();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application configurations. Please contact your system administrator.");
                _page.LoadList = ds;
            }
            return _page.LoadList;
        }

        public DataSet GetDocTypesList()
        {
            var _page = new PageBase();
            if (_page.DocumentTypes == null)
            {
                var ds = Admin.GetAllAttachmentTypes();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application configurations. Please contact your system administrator.");
                _page.DocumentTypes = ds;
            }
            return _page.DocumentTypes;
        }

        public DataSet GetUniqueOrganizationList()
        {
            var _page = new PageBase();
            if (_page.OrganizationsList == null)
            {
                var ds = Dal.GetOrganizationsList().ToList().ToDataSet();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application configurations. Please contact your system administrator.");
                _page.OrganizationsList = ds;
            }
            return _page.OrganizationsList;
        }

        public List<spGetJustifications_Result> GetJustificationValues()
        {
            var justifications = Dal.GetJustifications().ToList();
            ApplicationAssert.CheckCondition(justifications.Any(), "There is the problem to load application values. Please contact your system administrator.");
            return justifications;
        }

        public DataTable GetJustificationDefaultList()
        {
            var _page = new PageBase();
            if (_page.DefaultJustificationValues == null)
            {
                var ds = Dal.GetDefaultJustifications().ToList().ToDataSet();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application values. Please contact your system administrator.");

                var dt = ds.Tables[0];
                var dr = dt.NewRow();
                dr["Justification"] = 0;
                dr["JustificationDescription"] = "";
                dr["InDefaultList"] = 1;
                dr["DisplayAddOnField"] = 0;
                dr["AddOnDescription"] = "";
                dt.Rows.InsertAt(dr, 0);

                _page.DefaultJustificationValues = dt;
            }
            return _page.DefaultJustificationValues;
        }

        public DataTable GetActiveCodeList()
        {
            var _page = new PageBase();
            if (_page.ActiveCodesList == null)
            {
                var ds = Dal.GetActiveCodeList().ToList().ToDataSet();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application values. Please contact your system administrator.");

                var dt = ds.Tables[0];
                var dr = dt.NewRow();
                dr["Code"] = "";
                dr["CodeDefinition"] = "";
                dr["Valid"] = 0;
                dr["ActivationDate"] = DateTime.MinValue;
                dr["ExpirationDate"] = DateTime.MaxValue;
                dt.Rows.InsertAt(dr, 0);

                _page.ActiveCodesList = dt;
            }
            return _page.ActiveCodesList;
        }

        public DataTable GetActiveAndExpiredCodeList()
        {
            var _page = new PageBase();
            if (_page.ActiveAndExpiredCodesList == null)
            {
                var ds = Dal.GetCodeList().ToList().ToDataSet();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application values. Please contact your system administrator.");

                var dt = ds.Tables[0];
                var dr = dt.NewRow();
                dr["Code"] = "";
                dr["CodeDefinition"] = "";
                dr["Valid"] = 0;
                dr["ActivationDate"] = DateTime.MinValue;
                dr["ExpirationDate"] = DateTime.MaxValue;
                dt.Rows.InsertAt(dr, 0);

                _page.ActiveAndExpiredCodesList = dt;
            }
            return _page.ActiveAndExpiredCodesList;
        }

        public DataView GetValidationValuesList()
        {
            var _page = new PageBase();
            if (_page.ValidationValues == null)
            {
                var ds = Dal.GetValidationValues().ToList().ToDataSet();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application values. Please contact your system administrator.");

                _page.ValidationValues = ds.Tables[0].DefaultView;
            }
            return _page.ValidationValues;
        }

        public DataView GetContactsRoleList()
        {
            var _page = new PageBase();
            if (_page.ContactRolesList == null)
            {
                var ds = Dal.GetContactsRoles().ToList().ToDataSet();
                ApplicationAssert.CheckCondition(ds != null && ds.Tables[0].Rows.Count > 0, "There is the problem to load application values. Please contact your system administrator.");
                var dt = ds.Tables[0];
                var dr = dt.NewRow();
                dr["RoleCode"] = 0;
                dr["RoleDescription"] = "-- please select --";
                dt.Rows.InsertAt(dr, 0);

                _page.ContactRolesList = dt.DefaultView;
            }
            return _page.ContactRolesList;
        }

        public string GetOrganizationByOrgCode(string OrgCode)
        {
            var wholeOrgs = Dal.GetWholeOrgList().ToList();
            ApplicationAssert.CheckCondition(wholeOrgs.Any(), "There is the problem to load application values. Please contact your system administrator.");
            return wholeOrgs.First(o => o.OrgCode == OrgCode).Organization;
        }
    }
}