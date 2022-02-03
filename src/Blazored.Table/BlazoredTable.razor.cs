﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Blazored.Table.Models;

namespace Blazored.Table
{
    public partial class BlazoredTable
    {
        private Type _type;

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string Assembly { get; set; } = string.Empty;

        [Parameter]
        public string Method { get; set; } = string.Empty;

        [Parameter]
        public Type Type { get => _type; set => _type = value; }

        [Parameter]
        public ObservableCollection<object> DataSource { get; set; }

        [Parameter]
        public ObservableCollection<TableColumn> TableColumns { get; set; } // = new ObservableCollection<TableColumn> { new TableColumn() { title = "", data = "id" }, new TableColumn() { title = "First Name", data = "firstName" }, new TableColumn() { title = "Last Name", data = "lastName" } };

        [Parameter]
        public Settings Settings { get; set; }

        public string[] Columns => Type.GetProperties().Select(x => x.Name).ToArray();

        /*public string DatatableColumns => JsonConvert.SerializeObject(Type.GetProperties().Select(x => new TableColumn() { title = x.Name, data = x.Name }));

        public object[] Rows => DataSource?.Select(m => m).ToArray() ?? new object[] { };*/

        [Parameter]
        public string AjaxUrl { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (Settings == null)
            {
                Settings = new Settings()
                {
                    columns = TableColumns,
                    ordering = true,
                    deferRender = true,
                    scroller = true,
                    scrollY = "350px",
                    serverSide = true
                };
            }
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                var s = Settings;
                await JsRuntime.InvokeVoidAsync("BlazoredTable.create", Id, s, Assembly, Method);
            }
        }

        /*
         * SE SUPONE QUE AQUI SE LEE LA LISTA QUE VIENE DE GANSOFT
         * 
         */
        [JSInvokable]
        public static async Task<int[]> ReturnArrayAsync(AjaxViewModel data = null)
        {

            return await Task.FromResult(new int[] { 1, 2, 3 });
        }


    }
}
