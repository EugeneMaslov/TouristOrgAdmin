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
using TouristOrgAdmin.Views.Manager;
using TouristOrgAdmin.Views.Manager.Status;

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
        private Employees selectedEmployee;
        private Employees tempEmployee;
        private Statuses selectedStatus;
        private Statuses tempStatus;
        private bool isTrue = true;
        private bool isFalse = true;
        private bool isFar = true;

        public ObservableCollection<Communications> Communications { get; set; }
        public ObservableCollection<Employees> Employees { get; set; }
        public ObservableCollection<Statuses> Statuses { get; set; } 
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
        public RelayCommand ManagerModuleCommand { protected set; get; }
        public RelayCommand SelectEmployeeCommand { protected set; get; }
        public RelayCommand GoStatusCommand { protected set; get; }
        public RelayCommand EndManagerSubCommnad { protected set; get; }
        public RelayCommand SelectStatusCommand { protected set; get; }
        public RelayCommand CancelAddingStatusCommand { protected set; get; }
        public RelayCommand EndAddingStatusCommand { protected set; get; }
        public RelayCommand GoAddingStatusCommand { protected set; get; }
        public RelayCommand GoChangeStatusCommand { protected set; get; }
        public RelayCommand RemoveStatusCommand { protected set; get; }
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

        public Employees SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
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

        public Employees TempEmployee
        {
            get => tempEmployee;
            set
            {
                tempEmployee = value;
                OnPropertyChanged("TempEmployee");
            }
        }

        public Statuses SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                OnPropertyChanged("SelectedStatus");
            }
        }

        public Statuses TempStatus
        {
            get => tempStatus;
            set
            {
                tempStatus = value;
                OnPropertyChanged("TempStatus");
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
            ManagerModuleCommand = new RelayCommand(_ => GoManagerModule());
            SelectEmployeeCommand = new RelayCommand(_ => SelectEmployee());
            GoStatusCommand = new RelayCommand(_ => GoStatus());
            EndManagerSubCommnad = new RelayCommand(_ => EndManager());
            SelectStatusCommand = new RelayCommand(_ => SelectStatus());
            CancelAddingStatusCommand = new RelayCommand(_ => CancelAddingStatus());
            EndAddingStatusCommand = new RelayCommand(_ => EndAddingStatus());
            GoAddingStatusCommand = new RelayCommand(_ => GoAddingStatus());
            GoChangeStatusCommand = new RelayCommand(_ => GoChangingStatus());
            RemoveStatusCommand = new RelayCommand(_ => RemoveStatus());
            AdminAccount = AdminAccount.GetInstance();
            TempAdminAccount = new AdminAccount();
            Communications = new ObservableCollection<Communications>();
            Employees = new ObservableCollection<Employees>();
            Statuses = new ObservableCollection<Statuses>();
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
                if (MessageBox.Show((string)Application.Current.Resources["communication_remove_quest"], (string)Application.Current.Resources["communication_remove_text"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DB.Communications.Remove(SelectedCommunication);
                    DB.SaveChanges();
                    SelectedCommunication = null;
                    SelectCommunications();
                }
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

        private void GoManagerModule()
        {
            MainWindow.StaticNavigate(ManagerControl.GetInstance(this), this);
            SubContentPath = ManagerObserverControl.GetInstance(this);
        }

        public void SelectEmployee()
        {
            if (Like != null)
            {
                if (Employees != null)
                {
                    Employees.Clear();
                    while (Employees.Any())
                    {
                        Employees.RemoveAt(Employees.Count - 1);
                    }
                }

                IEnumerable<Employees> employees = null;

                if (Like == "")
                {
                    employees = DB.Employees.ToList();
                }
                else
                {
                    employees = DB.Employees.Where(x => x.FullName.Contains(Like) || x.Role.RoleName.Contains(Like) || x.Status.StatusName.Contains(Like));
                }

                foreach (Employees item in employees)
                {
                    if (!Employees.Contains(item))
                    {
                        Employees.Add(item);
                    }
                }
            }
        }

        private void GoStatus()
        {
            ManagerControl.StaticNavigate(StatusObserverControl.GetInstance(this), this);
        }

        private void EndManager()
        {
            ManagerControl.StaticNavigate(ManagerObserverControl.GetInstance(this), this);
            TempEmployee = null;
            SelectEmployee();
        }

        public void SelectStatus()
        {
            if (Like != null)
            {
                if (Statuses != null)
                {
                    Statuses.Clear();
                    while (Statuses.Any())
                    {
                        Statuses.RemoveAt(Statuses.Count - 1);
                    }
                }

                IEnumerable<Statuses> statuses = null;

                if (Like == "")
                {
                    statuses = DB.Statuses.ToList();
                }
                else
                {
                    statuses = DB.Statuses.Where(x => x.StatusName.Contains(Like));
                }

                foreach (Statuses item in statuses)
                {
                    if (!Statuses.Contains(item))
                    {
                        Statuses.Add(item);
                    }
                }
            }
        }

        private void GoAddingStatus()
        {
            ManagerControl.StaticNavigate(StatusAddingControl.GetInstance(this), this);
            TempStatus = new Statuses();
        }

        private void CancelAddingStatus()
        {
            ManagerControl.StaticNavigate(StatusObserverControl.GetInstance(this), this);
            TempStatus = null;
            SelectStatus();
        }

        private void EndAddingStatus()
        {
            if (tempStatus.StatusName != null && tempStatus.StatusName != "")
            {
                if (!DB.Statuses.Where(x => x.StatusID == tempStatus.StatusID).Any())
                {
                    tempStatus.StatusID = 0;
                    DB.Statuses.Add(tempStatus);
                }
                else
                {
                    SelectedStatus.StatusName = TempStatus.StatusName;
                    DB.Entry(SelectedStatus).State = EntityState.Modified;
                }
                DB.SaveChanges();
                SelectedStatus = null;
                TempStatus = null;
                CancelAddingStatus();
            }
            else
            {
                IsCommunicationError = true;
            }
        }

        private void GoChangingStatus()
        {
            if (SelectedStatus != null)
            {
                TempStatus = SelectedStatus.Clone() as Statuses;
                ManagerControl.StaticNavigate(StatusAddingControl.GetInstance(this), this);
            }
        }

        private void RemoveStatus()
        {
            if (SelectedStatus != null)
            {
                if (MessageBox.Show((string)Application.Current.Resources["status_remove_quest"], (string)Application.Current.Resources["status_remove_text"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DB.Statuses.Remove(SelectedStatus);
                    DB.SaveChanges();
                    SelectedStatus = null;
                    SelectStatus();
                }
            }
        }
    }
}
