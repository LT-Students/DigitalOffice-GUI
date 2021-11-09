using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace LT.DigitalOffice.GUI.Pages.Messenger.Workspace
{
	public partial class CreateWorkspace
	{
		public bool IsOpenCreateWorkspaceWindow { get; set; }

		private string _infoMessage;
		private bool _isSuccessOperation;

		private CreateWorkspaceRequest _request { get; set; }

		private ElementReference _descriptionInput;

		protected override void OnInitialized()
		{
			_request = new();
		}

		public async Task HandleValidSubmit()
		{
			try
			{
				OperationResultResponse response = await _messageService.CreateWorkspaceAsync(_request);

				if (response.Status == OperationResultStatusType.FullSuccess)
				{
					_infoMessage = "Workspace created successfully!";
					_isSuccessOperation = true;

					return;
				}

				_infoMessage = $"Errors: { string.Join(", ", response.Errors) }";
			}
			catch (ApiException<OperationResultResponse> ex)
			{
				_isSuccessOperation = false;
				_infoMessage = $"Something went wrong, please try again later.\nMessage: { ex.Result.Errors }";
			}
			catch (Exception ex)
			{
				_isSuccessOperation = false;
				_infoMessage = ex.Message;
			}
		}

		private async Task LoadFile(InputFileChangeEventArgs e)
		{
			ImageConsist image = new();

			foreach (var file in e.GetMultipleFiles())
			{
				try
				{
					List<string> imageData = new List<string>(file.Name.Split("."));
					image.Extension = string.Join("", ".", imageData.Last());

					var fileContent = new StreamContent(file.OpenReadStream());
					image.Content = Convert.ToBase64String(await fileContent.ReadAsByteArrayAsync());
				}
				catch (Exception ex)
				{
					_infoMessage = "Image did not load, please try again later.";
				}
			}

			_request.Image = image;
			StateHasChanged();
		}
	}
}
