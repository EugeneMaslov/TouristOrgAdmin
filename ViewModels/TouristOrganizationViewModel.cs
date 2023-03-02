using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TouristOrgAdmin.Core;
using TouristOrgAdmin.Models;
using TouristOrgAdmin.Controllers;
using System.Linq;
using TouristOrgAdmin.Views;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;

namespace TouristOrgAdmin.ViewModels
{
    public class TouristOrganizationViewModel : BaseViewModel, ILanguages
    {
        private bool isErrorOnLogin;
        private bool isErrorPassword;
        private bool isErrorOnRegister;
        private bool isUnknownError;
        private bool isCommunicationError;
        private TouristCompanyContext db = new TouristCompanyContext();
        private string errorText = "";
        private AdminAccount adminAccount;
        private Communications selectedCommunication;
        private Communications tempCommunication;
        private bool isTrue = true;
        private bool isFalse = true;
        private bool isFar = true;

        public ObservableCollection<Communications> Communications { get; set; }
        public AdminAccount TempAdminAccount { get; set; }
        public RelayCommand LoginCommand { protected set; get; }
        public RelayCommand RegisterCommand { protected set; get; }
        public RelayCommand LogOutCommand { protected set; get; }
        public RelayCommand ResetAccountCommand { protected set; get; }
        public RelayCommand BackCommand { protected set; get; }
        public RelayCommand ChangeResponsobilitesCommand { protected set; get; }
        public RelayCommand ChangePasswordCommand { protected set; get; }
        public RelayCommand ChangeAccountCommand { protected set; get; }
        public RelayCommand ChangePasswordEndCommand { protected set; get; }
        public RelayCommand OrganizationLinkCommand { protected set; get; }
        public RelayCommand SelectCommunication { protected set; get; }
        public RelayCommand AddCommunicationCommand { protected set; get; }
        public RelayCommand CancelCommunicationCommand { protected set; get; }
        public RelayCommand EndAddingCommunicationCommand { protected set; get; }
        public RelayCommand RemoveRelationCommand { protected set; get; }
        public RelayCommand LoadFileCommand { protected set; get; }
        public RelayCommand UpdateCommunicationCommand { protected set; get; }
        public string TempString { get; set; }
        public string Like { get; set; } = "";
        
        public bool IsTrue
        {
            get => isTrue;
            set
            {
                isTrue = value;
                SelectCommunications();
                OnPropertyChanged("IsTrue");
            }
        }
        public bool IsFalse
        {
            get => isFalse;
            set
            {
                isFalse = value;
                SelectCommunications();
                OnPropertyChanged("IsFalse");
            }
        }
        public bool IsFar
        {
            get => isFar;
            set
            {
                isFar = value;
                SelectCommunications();
                OnPropertyChanged("IsFar");
            }
        }

        public TouristCompanyContext DB
        {
            get => db;
            set
            {
                if (value != null)
                {
                    db = value;
                }
            }
        }

        public AdminAccount AdminAccount
        {
            get => adminAccount;
            set
            {
                adminAccount = value;
                OnPropertyChanged("AdminAccount");
            }
        }

        public Communications SelectedCommunication
        {
            get => selectedCommunication;
            set
            {
                selectedCommunication = value;
                OnPropertyChanged("SelectedCommunication");
            }
        }

        public Communications TempCommunication
        {
            get => tempCommunication;
            set
            {
                tempCommunication = value;
                OnPropertyChanged("TempCommunication");
            }
        }

        public bool IsErrorOnLogin
        {
            get => isErrorOnLogin;
            set
            {
                isErrorOnLogin = value;
                ErrorText = isErrorOnLogin ? (string)Application.Current.Resources["login_notFound"] : "";
            }
        }

        public bool IsUnknownError
        {
            get => isUnknownError;
            set
            {
                isUnknownError = value;
                ErrorText = isUnknownError ? (string)Application.Current.Resources["register_error"] : "";
            }
        }

        public bool IsErrorOnRegister
        {
            get => isErrorOnRegister;
            set
            {
                isErrorOnRegister = value;
                ErrorText = isErrorOnRegister ? (string)Application.Current.Resources["register_login_notCorrect"] : "";
            }
        }

        public bool IsErrorPassword
        {
            get => isErrorPassword;
            set
            {
                isErrorPassword = value;
                ErrorText = isErrorPassword ? (string)Application.Current.Resources["password_notCorrect"] : "";
            }
        }

        public bool IsCommunicationError
        {
            get => isCommunicationError;
            set
            {
                isCommunicationError = value;
                ErrorText = IsCommunicationError ? (string)Application.Current.Resources["communication_notCorrect"] : "";
            }
        }

        public string ErrorText
        {
            get => errorText;
            set
            {
                errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        public TouristOrganizationViewModel()
        {
            LoginCommand = new RelayCommand(_ => DoLogin());
            RegisterCommand = new RelayCommand(_ => DoRegister());
            LogOutCommand = new RelayCommand(_ => LogOut());
            ResetAccountCommand = new RelayCommand(_ => ResetAccount());
            BackCommand = new RelayCommand(_ => Back());
            ChangeAccountCommand = new RelayCommand(_ => ChangeAccount());
            ChangeResponsobilitesCommand = new RelayCommand(_ => ChangeResponsobilities());
            ChangePasswordCommand = new RelayCommand(_ => ChangePassword());
            ChangePasswordEndCommand = new RelayCommand(_ => ChangePasswordEnd());
            OrganizationLinkCommand = new RelayCommand(_ => GoOrganizationLink());
            SelectCommunication = new RelayCommand(_ => SelectCommunications());
            AddCommunicationCommand = new RelayCommand(_ => AddCommunication());
            CancelCommunicationCommand = new RelayCommand(_ => CancelSub());
            EndAddingCommunicationCommand = new RelayCommand(_ => EndAddingCommunication());
            RemoveRelationCommand = new RelayCommand(_ => RemoveRelation());
            LoadFileCommand = new RelayCommand(_ => LoadFile());
            UpdateCommunicationCommand = new RelayCommand(_ => UpdateCommunication());
            AdminAccount = AdminAccount.GetInstance();
            TempAdminAccount = new AdminAccount();
            Communications = new ObservableCollection<Communications>();
        }

        private void DoLogin()
        {
            
            AdminAccount admin = DB.AdminAccount.SingleOrDefault(c => c.Login == AdminAccount.Login);
            if (admin != null)
            {
                if (admin.IsSame(TempString))
                {
                    AdminAccount.RemoveInstance();
                    AdminAccount.SetInstance(admin);
                    AdminAccount = AdminAccount.GetInstance();
                    MainWindow.StaticNavigate(AdminPanel.GetInstance(this), this);
                }
                else
                {
                    IsErrorPassword = true;
                }
            }
            else
            {
                IsErrorOnLogin = true;
            }
        }

        private void DoRegister()
        {
            if (DB.AdminAccount.Count() == 0)
            {
                if (AdminAccount.Login != ""  && AdminAccount.Login != null)
                {
                    if (AdminAccount.Password != "" && AdminAccount.Password != null)
                    {
                        DB.AdminAccount.Add(AdminAccount);
                        DB.SaveChanges();
                        MainWindow.StaticNavigate(AdminPanel.GetInstance(this), this);
                    }
                    else
                    {
                        IsErrorPassword = true;
                    }
                }
                else
                {
                    IsErrorOnRegister = true;
                }
            }
            else
            {
                IsUnknownError = true;
            }
        }

        private void LogOut()
        {
            if (AdminAccount != null)
            {
                MainWindow.StaticNavigate(LoginControl.GetInstance(this), this);
            }
        }

        private void ChangePassword()
        {
            MainWindow.StaticNavigate(ChangePasswordControl.GetInstance(this), this);
        }

        private void ChangePasswordEnd()
        {
            if (TempAdminAccount != null)
            {
                AdminAccount etalon = DB.AdminAccount.SingleOrDefault(c => c.Login == AdminAccount.Login);
                if (etalon != null)
                {
                    if (etalon.Password == TempAdminAccount.Password)
                    {
                        AdminAccount.Password = TempString;
                        ChangeAccount();
                    }
                    else
                    {
                        IsErrorPassword = true;
                    }
                }
                else
                {
                    IsErrorOnLogin = true;
                }
            }
        }

        private void ResetAccount()
        {
            if (AdminAccount != null)
            {
                if (MessageBox.Show((string)Application.Current.Resources["acc_reset_quest"], (string)Application.Current.Resources["acc_reset"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DB.AdminAccount.Remove(AdminAccount);
                    DB.SaveChanges();
                    MainWindow.StaticNavigate(RegisterControl.GetInstance(this), this);
                }
            }
        }

        private void ChangeResponsobilities()
        {
            MainWindow.StaticNavigate(ResponsobilitiesAdd.GetInstance(this), this);
        }

        private void Back()
        {
            MainWindow.StaticNavigate(AdminPanel.GetInstance(this), this);
            SubContentPath = null;
        }

        private void ChangeAccount()
        {
            if (AdminAccount != null)
            {
                DB.AdminAccount.Update(AdminAccount);
                DB.SaveChanges();
            }
            Back();
        }

        private void GoOrganizationLink()
        {
            MainWindow.StaticNavigate(OrganizationLinksControl.GetInstance(this), this);
            SubContentPath = OrgLinksObserverControl.GetInstance(this);
        }

        public void LanguageChanged()
        {
            if (ContentPath != null)
            {
                ((ILanguages)ContentPath).LanguageChanged();
            }
            if (SubContentPath != null)
            {
                ((ILanguages)SubContentPath).LanguageChanged();
            }
        }

        public void SelectCommunications()
        {
            if (Like != null)
            {
                if (Communications != null)
                {
                    Communications.Clear();
                    while (Communications.Any())
                    {
                        Communications.RemoveAt(Communications.Count - 1);
                    }
                }

                IEnumerable<Communications> commmunications = null;

                if (Like == "")
                {
                    commmunications = DB.Communications.ToList();
                }
                else
                {
                    commmunications = DB.Communications.Where(x => x.Name.Contains(Like) || x.UNP.Contains(Like));
                }
                string istrue = (string)Application.Current.Resources["relation_true"];
                string isfalse = (string)Application.Current.Resources["relation_false"];
                string isfar = (string)Application.Current.Resources["relation_far"];

                foreach (Communications item in commmunications)
                {
                    if ((item.State() == istrue && IsTrue) || (item.State() == isfalse && IsFalse) || (item.State() == isfar && IsFar))
                    {
                        if (!Communications.Contains(item))
                        {
                            Communications.Add(item);
                        }
                    }
                }

            }
        }

        private void CancelSub()
        {
            OrganizationLinksControl.StaticNavigate(OrgLinksObserverControl.GetInstance(this), this);
            TempCommunication = null;
            SelectCommunications();
        }

        private void AddCommunication()
        {
            OrganizationLinksControl.StaticNavigate(AddingCommunicationControl.GetInstance(this), this);
            TempCommunication = new Communications("", "", DateTime.Now, DateTime.Now.AddDays(1), "", "");
        }

        private void UpdateCommunication()
        {
            if (SelectedCommunication != null)
            {
                TempCommunication = SelectedCommunication.Clone() as Communications;
                OrganizationLinksControl.StaticNavigate(AddingCommunicationControl.GetInstance(this), this);
            }
        }

        private void EndAddingCommunication()
        {
            if (tempCommunication.UNP != null && tempCommunication.UNP != "" && tempCommunication.Name != null && tempCommunication.Name != "" && tempCommunication.DateStart <= tempCommunication.DateEnd)
            {
                if (!DB.Communications.Contains(tempCommunication))
                {
                    DB.Communications.Add(tempCommunication);
                }
                else
                {
                    SelectedCommunication.Name = TempCommunication.Name;
                    SelectedCommunication.UNP = TempCommunication.UNP;
                    SelectedCommunication.DateStart = TempCommunication.DateStart;
                    SelectedCommunication.DateEnd = TempCommunication.DateEnd;
                    SelectedCommunication.DocPathName = TempCommunication.DocPathName;
                    SelectedCommunication.FullDocPath = TempCommunication.FullDocPath;
                    DB.Entry(SelectedCommunication).State = EntityState.Modified;
                }
                DB.SaveChanges();
                SelectedCommunication = null;
                TempCommunication = null;
                CancelSub();
            }
            else
            {
                IsCommunicationError = true;
            }
        }

        private void RemoveRelation()
        {
            if (SelectedCommunication != null)
            {
                DB.Communications.Remove(SelectedCommunication);
                DB.SaveChanges();
                SelectedCommunication = null;
                SelectCommunications();
            }
        }

        private void LoadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Documents (*.doc;*.docx;*.docm)|*.doc;*.docx;*.docm|Documents (*.pdf)|*.pdf|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string[] str = openFileDialog.FileName.Split("\\");
                if (TempCommunication != null)
                {
                    
                    string docpathname = str[str.Length - 1];
                    string copied = Directory.GetCurrentDirectory() + "\\Docs";
                    if (!Directory.Exists(copied))
                    {
                        Directory.CreateDirectory(copied);
                    }
                    string fullpathname = copied + "\\" + docpathname;
                    if (!File.Exists(fullpathname))
                    {
                        File.Copy(openFileDialog.FileName, fullpathname);
                    }
                    Communications temp = new Communications(TempCommunication.Name, TempCommunication.UNP, TempCommunication.DateStart, TempCommunication.DateEnd, docpathname, fullpathname);
                    TempCommunication = temp;
                }
            }
        }

        public void OpenFile()
        {
            if (SelectedCommunication != null)
            {
                if (!File.Exists(SelectedCommunication.FullDocPath))
                {
                    SelectedCommunication.FullDocPath = Directory.GetCurrentDirectory() + "\\Docs\\" + SelectedCommunication.DocPathName;
                    if (!File.Exists(SelectedCommunication.FullDocPath))
                    {
                        return;
                    }
                }
                var proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = SelectedCommunication.FullDocPath;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
        }
    }
}
