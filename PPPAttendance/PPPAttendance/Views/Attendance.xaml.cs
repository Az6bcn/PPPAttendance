using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Data.Tables;
using PPPAttendance.Dtos;
using PPPAttendance.Models;
using PPPAttendance.Services.TableServiceClient;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace PPPAttendance.Views
{
    public partial class Attendance : ContentPage
    {
        public readonly string today;
        private string _service;
        public Attendance(string service)
        {
            InitializeComponent();

            _service = service;

            today = DateTime.Now.ToString("D");

            BindingContext = this;

            Device.BeginInvokeOnMainThread(() =>
            {
                labelDate.Text = today;
            });
        }

        async void ManPlus_OnClicked(object sender,
                                            EventArgs e)
        {
            var client = new AzureTableClient();

            var entity = AttendanceEntity(_service);

            var entityInDb = await client.GetEntity(entity);

            if (entityInDb is null)
            {
                entity.Men = 1;
                await client.AddEntityAsync(entity);
            }

            if (entityInDb is not null)
            {
                entityInDb.Men += 1;
                await client.UpdateEntityAsync(entityInDb);
                await MaterialDialog.Instance.SnackbarAsync(message: "Man Counted, + 1", 
                                                            msDuration: MaterialSnackbar.DurationShort,
                                                            new MaterialSnackbarConfiguration
                                                            {
                                                                BackgroundColor = Color.Black
                                                            });
            }
                
        }
        
        async void ManMinus_OnClicked(object sender,
                                     EventArgs e)
        {
            var client = new AzureTableClient();

            var entity = AttendanceEntity(_service);

            var entityInDb = await client.GetEntity(entity);

            if (entityInDb is null)
                return;

            if (entityInDb is not null)
            {
                entityInDb.Men -= 1;
                await client.UpdateEntityAsync(entityInDb);
                
                await MaterialDialog.Instance.SnackbarAsync(message: "Man deducted, - 1", 
                                                            msDuration: MaterialSnackbar.DurationShort,
                                                            new MaterialSnackbarConfiguration
                                                            {
                                                                BackgroundColor = Color.IndianRed
                                                            });
            }
        }
        
        async void WomanPlus_OnClicked(object sender,
                                            EventArgs e)
        {
            var client = new AzureTableClient();

            var entity = AttendanceEntity(_service);

            var entityInDb = await client.GetEntity(entity);

            if (entityInDb is null)
            {
                entity.Women = 1;
                await client.AddEntityAsync(entity);
            }

            if (entityInDb is not null)
            {
                entityInDb.Women += 1;
                await client.UpdateEntityAsync(entityInDb);
                await MaterialDialog.Instance.SnackbarAsync(message: "Woman Counted, + 1", 
                                                            msDuration: MaterialSnackbar.DurationShort,
                                                            new MaterialSnackbarConfiguration
                                                            {
                                                                BackgroundColor = Color.Black
                                                            });
            }
                
        }
        
        async void WomanMinus_OnClicked(object sender,
                                     EventArgs e)
        {
            var client = new AzureTableClient();

            var entity = AttendanceEntity(_service);

            var entityInDb = await client.GetEntity(entity);

            if (entityInDb is null)
                return;

            if (entityInDb is not null)
            {
                entityInDb.Women -= 1;
                await client.UpdateEntityAsync(entityInDb);
                
                await MaterialDialog.Instance.SnackbarAsync(message: "Woman deducted, - 1", 
                                                            msDuration: MaterialSnackbar.DurationShort,
                                                            new MaterialSnackbarConfiguration
                                                            {
                                                                BackgroundColor = Color.IndianRed
                                                            });
            }
        }
        
        async void ChildPlus_OnClicked(object sender,
                                            EventArgs e)
        {
            var client = new AzureTableClient();

            var entity = AttendanceEntity(_service);

            var entityInDb = await client.GetEntity(entity);

            if (entityInDb is null)
            {
                entity.Children = 1;
                await client.AddEntityAsync(entity);
            }

            if (entityInDb is not null)
            {
                entityInDb.Children += 1;
                await client.UpdateEntityAsync(entityInDb);
                await MaterialDialog.Instance.SnackbarAsync(message: "Child Counted, + 1", 
                                                            msDuration: MaterialSnackbar.DurationShort,
                                                            new MaterialSnackbarConfiguration
                                                            {
                                                                BackgroundColor = Color.Black
                                                            });
            }
                
        }
        
        async void ChildMinus_OnClicked(object sender,
                                     EventArgs e)
        {
            var client = new AzureTableClient();

            var entity = AttendanceEntity(_service);

            var entityInDb = await client.GetEntity(entity);

            if (entityInDb is null)
                return;

            if (entityInDb is not null)
            {
                entityInDb.Children -= 1;
                await client.UpdateEntityAsync(entityInDb);
                
                await MaterialDialog.Instance.SnackbarAsync(message: "Child deducted, - 1", 
                                                            msDuration: MaterialSnackbar.DurationShort,
                                                            new MaterialSnackbarConfiguration
                                                            {
                                                                BackgroundColor = Color.IndianRed
                                                            });
            }
        }

        private static AttendanceEntity AttendanceEntity(string service)
        {
            var entity = new Models.AttendanceEntity()
            {
                PartitionKey = TableSettings.PartitionKey(service),
                RowKey = TableSettings.RowKey(service),
                Date = DateTime.UtcNow.Date,
                Men = 0,
                Women = 0,
                Children = 0,
                Service = service
            };
            return entity;
        }
        
    }
}
