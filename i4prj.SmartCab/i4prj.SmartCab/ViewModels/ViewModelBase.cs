using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections;
using Prism.AppModel;

namespace i4prj.SmartCab.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IPageLifecycleAware
    {
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService DialogService { get; private set; }

        private string _title;

        /// <summary>
        /// Title of the view.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #region OwnProperties

        private bool _isBusy = false;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:i4prj.SmartCab.ViewModels.ViewModelBase"/> is busy currently sending a request.
        /// </summary>
        /// <value><c>true</c> if is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); RaisePropertyChanged(nameof(IsReady)); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:i4prj.SmartCab.ViewModels.ViewModelBase"/> is ready by not currently sending a request.
        /// </summary>
        /// <value><c>true</c> if is ready; otherwise, <c>false</c>.</value>
        public bool IsReady
        {
            get { return !_isBusy; }
            set { SetProperty(ref _isBusy, !value); RaisePropertyChanged(nameof(IsBusy)); }
        }

        #endregion

        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public virtual void OnAppearing()
        {

        }

        public virtual void OnDisappearing()
        {

        }
    }
}
