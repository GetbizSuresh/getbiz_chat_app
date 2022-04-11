﻿using getbiz_chat_app.Getbiz_DbContext;
using getbiz_chat_app.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace getbiz_chat_app.Repository.Group_Member_Users_Permissions
{
    public class GroupMemberUsersPermissionsRepository : IGroupMemberUsersPermissionsRepository
    {

        public Response UpdateGroupMemberUsersPermissions(group_member_users_permissions obj_group_member_users_permissions)
        {
            Response response = new Response();
            try
            {
                GetSetDatabase Obj_GetSetDatabase = new GetSetDatabase();
                var result = Obj_GetSetDatabase.UpdateGroupMemberUsersPermissions(obj_group_member_users_permissions);
                response = result;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }


        public Response GetChatIdGroupMemberUserPermission(string CustId, Int64 ChatId,Int64 UserId)
        {
            Response response = new Response();
            try
            {
                DataTable dset = new DataTable();

                GetSetDatabase Obj_getsetdb = new GetSetDatabase();
                dset = Obj_getsetdb.GetChatIdGroupMemberUserPermission(CustId, ChatId, UserId);

                List<group_member_users_permissions> obj_users_list = new List<group_member_users_permissions>();
                obj_users_list = ConvertDataTable<group_member_users_permissions>(dset);

                response.Data = (obj_users_list).ToList();
                //string JSONresult;
                //JSONresult = JsonConvert.SerializeObject(dset);
                //response.Data = JSONresult;
                response.Message = "Data Fetch successfully !!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;

        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }


        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
