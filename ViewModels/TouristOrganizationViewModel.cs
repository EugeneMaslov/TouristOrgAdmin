using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using TouristOrgAdmin.Core;
using TouristOrgAdmin.Views;
using TouristOrgAdmin.Models;
using TouristOrgAdmin.Controllers;
using TouristOrgAdmin.Views.Manager;
using TouristOrgAdmin.Views.Manager.Status;
using TouristOrgAdmin.Views.Manager.Role;
using TouristOrgAdmin.Views.Accountant;
using TouristOrgAdmin.Views.Accountant.Materials;
using TouristOrgAdmin.Views.Accountant.Taxes;

namespace TouristOrgAdmin.ViewModels
{
    public class TouristOrganizationViewModel : BaseViewModel, ILanguages
    {
        #region Fields
        private bool isErrorOnLogin;
        private bool isErrorPassword;
        private bool isErrorOnRegister;
        private bool isUnknownError;
        private bool isSettingsError;
        private TouristCompanyContext db = new TouristCompanyContext();
        private string errorText = "";
        private AdminAccount adminAccount;
        private Communications selectedCommunication;
        private Communications tempCommunication;
        private Employees selectedEmployee;
        private Employees tempEmployee;
        private Statuses selectedStatus;
        private Statuses tempStatus;
        private Roles selectedRole;
        private Roles tempRole;
        private Material selectedMaterial;
        private Material tempMaterial;
        private Tax selectedTax;
        private Tax tempTax;
        private FixedSource selectedFixedSource;
        private FixedSource tempFixedSource;
        private bool isTrue = true;
        private bool isFalse = true;
        private bool isFar = true;
        #endregion
        #region Properties
        public ObservableCollection<Communications> Communications { get; set; }
        public ObservableCollection<Employees> Employees { get; set; }
        public ObservableCollection<Statuses> Statuses { get; set; }
        public ObservableCollection<Roles> Roles { get; set; }
        public ObservableCollection<Material> Materials { get; set; }
        public ObservableCollection<Tax> Taxes { get; set; }
        public ObservableCollection<FixedSource> FixedSources { get; set; }
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
        public RelayCommand SelectRolesCommand { protected set; get; }
        public RelayCommand GoRolesCommand { protected set; get; }
        public RelayCommand GoRolesAddingCommand { protected set; get; }
        public RelayCommand CancelAddingRolesCommand { protected set; get; }
        public RelayCommand EndAddingRoleCommnad { protected set; get; }
        public RelayCommand GoChangeRoleCommand { protected set; get; }
        public RelayCommand RemoveRoleCommand { protected set; get; }
        public RelayCommand GoAddingManagerCommand { protected set; get; }
        public RelayCommand CancelAddingManagerCommand { protected set; get; }
        public RelayCommand EndAddingManagerCommand { protected set; get; }
        public RelayCommand GoChangeManagerCommand { protected set; get; }
        public RelayCommand RemoveManagerCommand { protected set; get; }
        public RelayCommand AccountantModuleCommand { protected set; get; }
        public RelayCommand GoMaterialsCommand { protected set; get; }
        public RelayCommand SelectMaterialCommand { protected set; get; }
        public RelayCommand BackToAccountantCommand { protected set; get; }
        public RelayCommand GoAddingMaterialsCommand { protected set; get; }
        public RelayCommand CancelAddingMaterialsCommand { protected set; get; }
        public RelayCommand EndAddingMaterialsCommand { protected set; get; }
        public RelayCommand GoChangeMaterialCommand { protected set; get; }
        public RelayCommand RemoveMaterialCommand { protected set; get; }
        public RelayCommand GoTaxesCommand { protected set; get; }
        public RelayCommand SelectTaxesCommand { protected set; get; }
        public RelayCommand GoAddingTaxCommand { protected set; get; }
        public RelayCommand CancelAddingTaxCommand { protected set; get; }
        public RelayCommand EndAddingTaxCommand { protected set; get; }
        public RelayCommand GoChangeTaxCommand { protected set; get; }
        public RelayCommand RemoveTaxCommand { protected set; get; }
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

        public Roles SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                OnPropertyChanged("SelectedRole");
            }
        }

        public Roles TempRole
        {
            get => tempRole;
            set
            {
                tempRole = value;
                OnPropertyChanged("TempRole");
            }
        }

        public Material SelectedMaterial
        {
            get => selectedMaterial;
            set
            {
                selectedMaterial = value;
                OnPropertyChanged("SelectedMaterial");
            }
        }

        public Material TempMaterial
        {
            get => tempMaterial;
            set
            {
                tempMaterial = value;
                OnPropertyChanged("TempMaterial");
            }
        }

        public Tax SelectedTax
        {
            get => selectedTax;
            set
            {
                selectedTax = value;
                OnPropertyChanged("SelectedTax");
            }
        }

        public Tax TempTax
        {
            get => tempTax;
            set
            {
                tempTax = value;
                OnPropertyChanged("TempTax");
            }
        }

        public FixedSource SelectedFixedSource
        {
            get => selectedFixedSource;
            set
            {
                selectedFixedSource = value;
                OnPropertyChanged("SelectedFixedSource");
            }
        }

        public FixedSource TempFixedSource
        {
            get => tempFixedSource;
            set
            {
                tempFixedSource = value;
                OnPropertyChanged("TempFixedSource");
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

        public bool IsSettingsError
        {
            get => isSettingsError;
            set
            {
                isSettingsError = value;
                ErrorText = IsSettingsError ? (string)Application.Current.Resources["communication_notCorrect"] : "";
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
        #endregion
        #region Init
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
            SelectRolesCommand = new RelayCommand(_ => SelectRoles());
            GoRolesCommand = new RelayCommand(_ => GoRoles());
            GoRolesAddingCommand = new RelayCommand(_ => GoAddingRole());
            CancelAddingRolesCommand = new RelayCommand(_ => CancelAddingRole());
            EndAddingRoleCommnad = new RelayCommand(_ => EndAddingRole());
            GoChangeRoleCommand = new RelayCommand(_ => GoChangingRoles());
            RemoveRoleCommand = new RelayCommand(_ => RemoveRole());
            GoAddingManagerCommand = new RelayCommand(_ => GoAddingManager());
            CancelAddingManagerCommand = new RelayCommand(_ => CancelAddingManager());
            EndAddingManagerCommand = new RelayCommand(_ => EndAddingManager());
            GoChangeManagerCommand = new RelayCommand(_ => GoChangingManager());
            RemoveManagerCommand = new RelayCommand(_ => RemoveManager());
            AccountantModuleCommand = new RelayCommand(_ => GoAccountantModule());
            GoMaterialsCommand = new RelayCommand(_ => GoMaterials());
            SelectMaterialCommand = new RelayCommand(_ => SelectMaterials());
            BackToAccountantCommand = new RelayCommand(_ => BackToAccountant());
            GoAddingMaterialsCommand = new RelayCommand(_ => GoAddingMaterials());
            CancelAddingMaterialsCommand = new RelayCommand(_ => CancelAddingMaterials());
            EndAddingMaterialsCommand = new RelayCommand(_ => EndAddingMaterials());
            GoChangeMaterialCommand = new RelayCommand(_ => GoChangeMaterial());
            RemoveMaterialCommand = new RelayCommand(_ => RemoveMaterial());
            GoTaxesCommand = new RelayCommand(_ => GoTaxes());
            SelectTaxesCommand = new RelayCommand(_ => SelectTaxes());
            GoAddingTaxCommand = new RelayCommand(_ => GoAddingTax());
            CancelAddingTaxCommand = new RelayCommand(_ => CancelAddingTax());
            EndAddingTaxCommand = new RelayCommand(_ => EndAddingTax());
            GoChangeTaxCommand = new RelayCommand(_ => GoChangeTax());
            RemoveTaxCommand = new RelayCommand(_ => RemoveTax());
            AdminAccount = AdminAccount.GetInstance();
            TempAdminAccount = new AdminAccount();
            Communications = new ObservableCollection<Communications>();
            Employees = new ObservableCollection<Employees>();
            Statuses = new ObservableCollection<Statuses>();
            Roles = new ObservableCollection<Roles>();
            Materials = new ObservableCollection<Material>();
            Taxes = new ObservableCollection<Tax>();
            FixedSources = new ObservableCollection<FixedSource>();
        }
        #endregion
        #region Login&Register
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
                    if (TempString != "" && TempString != null)
                    {
                        AdminAccount.Password = TempString;
                        if (AdminAccount.Password != null && AdminAccount.Password != "")
                        {
                            DB.AdminAccount.Add(AdminAccount);
                            DB.SaveChanges();
                            MainWindow.StaticNavigate(AdminPanel.GetInstance(this), this);
                        }
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
        #endregion
        #region AccountChange
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
        #endregion
        #region Language
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
        #endregion
        #region ModuleCommunications
        private void GoOrganizationLink()
        {
            MainWindow.StaticNavigate(OrganizationLinksControl.GetInstance(this), this);
            SubContentPath = OrgLinksObserverControl.GetInstance(this);
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
                IsSettingsError = true;
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
        #endregion
        #region ModuleManager
        #region Employees
        private void GoManagerModule()
        {
            MainWindow.StaticNavigate(ManagerControl.GetInstance(this), this);
            SubContentPath = ManagerObserverControl.GetInstance(this);
            SelectStatus();
            SelectRoles();
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

        private void GoAddingManager()
        {
            ManagerControl.StaticNavigate(ManagerAddingControl.GetInstance(this), this);
            TempEmployee = new Employees(-1, "", DateTime.Now, DateTime.Now, null);
            SelectStatus();
            SelectRoles();
            SelectedRole = null;
            SelectedStatus = null;
        }

        private void CancelAddingManager()
        {
            ManagerControl.StaticNavigate(ManagerObserverControl.GetInstance(this), this);
            TempEmployee = null;
            TempString = "";
            SelectEmployee();
        }

        private void EndAddingManager()
        {

            if (SelectedRole != null)
            {
                tempEmployee.Role = SelectedRole;
                tempEmployee.RoleID = SelectedRole.RoleID;
            }
            if (SelectedStatus != null)
            {
                tempEmployee.Status = SelectedStatus;
                tempEmployee.StatusID = SelectedStatus.StatusID;
            }
            if (TempString != "" && TempString != null)
            {
                tempEmployee.Password = TempString;
            }
            if (tempEmployee.FullName != null && tempEmployee.FullName != "" && tempEmployee.Password != null && tempEmployee.Password != "" && tempEmployee.Role != null && tempEmployee.Status != null)
            {
                if (!DB.Employees.Where(x => x.EmpID == tempEmployee.EmpID).Any())
                {
                    tempEmployee.EmpID = 0;
                    DB.Employees.Add(tempEmployee);
                }
                else
                {
                    SelectedEmployee.FullName = TempEmployee.FullName;
                    if (TempString != "" && TempString != null)
                    {
                        SelectedEmployee.Password = TempString;
                    }
                    SelectedEmployee.BirthDate = TempEmployee.BirthDate;
                    SelectedEmployee.EmpDate = TempEmployee.EmpDate;
                    SelectedEmployee.Role = TempEmployee.Role;
                    SelectedEmployee.RoleID = TempEmployee.RoleID;
                    SelectedEmployee.Status = TempEmployee.Status;
                    SelectedEmployee.StatusID = TempEmployee.StatusID;
                    DB.Entry(SelectedEmployee).State = EntityState.Modified;
                }
                DB.SaveChanges();
                SelectedEmployee = null;
                TempEmployee = null;
                CancelAddingManager();
            }
            else
            {
                IsSettingsError = true;
            }
        }

        private void GoChangingManager()
        {
            if (SelectedEmployee != null)
            {
                TempEmployee = SelectedEmployee.Clone() as Employees;
                TempString = "";
                ManagerControl.StaticNavigate(ManagerAddingControl.GetInstance(this), this);
            }
        }
        private void RemoveManager()
        {
            if (SelectedEmployee != null)
            {
                if (MessageBox.Show((string)Application.Current.Resources["employee_remove_quest"], (string)Application.Current.Resources["employee_remove_text"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DB.Employees.Remove(SelectedEmployee);
                    DB.SaveChanges();
                    SelectedEmployee = null;
                    SelectEmployee();
                }
            }
        }
        #endregion
        #region Statuses
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
                IsSettingsError = true;
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
        #endregion
        #region Roles
        private void GoRoles()
        {
            ManagerControl.StaticNavigate(RolesObserverControl.GetInstance(this), this);
        }
        public void SelectRoles()
        {
            if (Like != null)
            {
                if (Roles != null)
                {
                    Roles.Clear();
                    while (Roles.Any())
                    {
                        Roles.RemoveAt(Roles.Count - 1);
                    }
                }

                IEnumerable<Roles> roles = null;

                if (Like == "")
                {
                    roles = DB.Roles.ToList();
                }
                else
                {
                    roles = DB.Roles.Where(x => x.RoleName.Contains(Like));
                }

                foreach (Roles item in roles)
                {
                    if (!Roles.Contains(item))
                    {
                        Roles.Add(item);
                    }
                }
            }
        }
        private void GoAddingRole()
        {
            ManagerControl.StaticNavigate(RolesAddingControl.GetInstance(this), this);
            TempRole = new Roles();
        }

        private void CancelAddingRole()
        {
            ManagerControl.StaticNavigate(RolesObserverControl.GetInstance(this), this);
            TempRole = null;
            SelectRoles();
        }

        private void EndAddingRole()
        {
            try
            {
                if (tempRole.RoleName != null && tempRole.RoleName != "")
                {
                    if (!DB.Roles.Where(x => x.RoleID == tempRole.RoleID).Any())
                    {
                        tempRole.RoleID = 0;
                        DB.Roles.Add(tempRole);
                    }
                    else
                    {
                        SelectedRole.Rate = TempRole.Rate;
                        SelectedRole.RoleName = TempRole.RoleName;
                        DB.Entry(SelectedRole).State = EntityState.Modified;
                    }
                    DB.SaveChanges();
                    SelectedRole = null;
                    TempRole = null;
                    CancelAddingRole();
                }
                else
                {
                    IsSettingsError = true;
                }
            }
            catch (Exception)
            {
                IsSettingsError = true;
            }
        }
        private void GoChangingRoles()
        {
            if (SelectedRole != null)
            {
                TempRole = SelectedRole.Clone() as Roles;
                ManagerControl.StaticNavigate(RolesAddingControl.GetInstance(this), this);
            }
        }
        private void RemoveRole()
        {
            if (SelectedRole != null)
            {
                if (MessageBox.Show((string)Application.Current.Resources["roles_remove_quest"], (string)Application.Current.Resources["roles_remove_text"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DB.Roles.Remove(SelectedRole);
                    DB.SaveChanges();
                    SelectedRole = null;
                    SelectRoles();
                }
            }
        }
        #endregion
        #endregion
        #region ModuleAccountant
        private void GoAccountantModule()
        {
            MainWindow.StaticNavigate(AccountantControl.GetInstance(this), this);
            SubContentPath = AccountantObserverControl.GetInstance(this);
        }

        private void BackToAccountant()
        {
            AccountantControl.StaticNavigate(AccountantObserverControl.GetInstance(this), this);
        }
        #region Materials
        private void GoMaterials()
        {
            AccountantControl.StaticNavigate(MaterialsObserverControl.GetInstance(this), this);
        }

        public void SelectMaterials()
        {
            if (Like != null)
            {
                if (Materials != null)
                {
                    Materials.Clear();
                    while (Materials.Any())
                    {
                        Materials.RemoveAt(Materials.Count - 1);
                    }
                }

                IEnumerable<Material> materials = null;

                if (Like == "")
                {
                    materials = DB.Materials.ToList();
                }
                else
                {
                    materials = DB.Materials.Where(x => x.MaterialName.Contains(Like));
                }

                foreach (Material item in materials)
                {
                    if (!Materials.Contains(item))
                    {
                        Materials.Add(item);
                    }
                }
            }
        }

        private void GoAddingMaterials()
        {
            AccountantControl.StaticNavigate(MaterialsAddingControl.GetInstance(this), this);
            TempMaterial = new Material();
        }
        private void CancelAddingMaterials()
        {
            AccountantControl.StaticNavigate(MaterialsObserverControl.GetInstance(this), this);
            TempMaterial = null;
            SelectMaterials();
        }

        private void EndAddingMaterials()
        {
            try
            {
                if (tempMaterial != null && tempMaterial.MaterialName != "" && tempMaterial.MaterialName != null)
                {
                    if (!DB.Materials.Where(x => x.MaterialID == tempMaterial.MaterialID).Any())
                    {
                        tempMaterial.MaterialID = 0;
                        DB.Materials.Add(tempMaterial);
                    }
                    else
                    {
                        SelectedMaterial.MaterialName = TempMaterial.MaterialName;
                        SelectedMaterial.Price = TempMaterial.Price;
                        SelectedMaterial.Count = TempMaterial.Count;
                        DB.Entry(SelectedMaterial).State = EntityState.Modified;
                    }
                    DB.SaveChanges();
                    SelectedMaterial = null;
                    TempMaterial = null;
                    CancelAddingMaterials();
                }
                else
                {
                    IsSettingsError = true;
                }
            }
            catch (Exception)
            {
                IsSettingsError = true;
            }
        }

        private void GoChangeMaterial()
        {
            if (SelectedMaterial != null)
            {
                TempMaterial = SelectedMaterial.Clone() as Material;
                AccountantControl.StaticNavigate(MaterialsAddingControl.GetInstance(this), this);
            }
        }

        private void RemoveMaterial()
        {
            if (SelectedMaterial != null)
            {
                if (MessageBox.Show((string)Application.Current.Resources["materials_remove_quest"], (string)Application.Current.Resources["materials_remove_text"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DB.Materials.Remove(SelectedMaterial);
                    DB.SaveChanges();
                    SelectedMaterial = null;
                    SelectMaterials();
                }
            }
        }
        #endregion
        #region Taxes
        private void GoTaxes()
        {
            AccountantControl.StaticNavigate(TaxesObserverControl.GetInstance(this), this);
        }

        public void SelectTaxes()
        {
            if (Like != null)
            {
                if (Taxes != null)
                {
                    Taxes.Clear();
                    while (Taxes.Any())
                    {
                        Taxes.RemoveAt(Taxes.Count - 1);
                    }
                }

                IEnumerable<Tax> taxes = null;

                if (Like == "")
                {
                    taxes = DB.Taxes.ToList();
                }
                else
                {
                    taxes = DB.Taxes.Where(x => x.NameTax.Contains(Like));
                }

                foreach (Tax item in taxes)
                {
                    if (!Taxes.Contains(item))
                    {
                        Taxes.Add(item);
                    }
                }
            }
        }

        private void GoAddingTax()
        {
            AccountantControl.StaticNavigate(TaxesAddingControl.GetInstance(this), this);
            TempTax = new Tax();
        }

        private void CancelAddingTax()
        {
            AccountantControl.StaticNavigate(TaxesObserverControl.GetInstance(this), this);
            TempTax = null;
            SelectTaxes();
        }

        private void EndAddingTax()
        {
            try
            {
                if (tempTax != null && tempTax.NameTax != "" && tempTax.NameTax != null)
                {
                    if (!DB.Taxes.Where(x => x.TaxID == tempTax.TaxID).Any())
                    {
                        tempTax.TaxID = 0;
                        DB.Taxes.Add(tempTax);
                    }
                    else
                    {
                        SelectedTax.NameTax = TempTax.NameTax;
                        SelectedTax.IsFixed = TempTax.IsFixed;
                        SelectedTax.Percent = TempTax.Percent;
                        SelectedTax.Price = TempTax.Price;
                        DB.Entry(SelectedTax).State = EntityState.Modified;
                    }
                    DB.SaveChanges();
                    SelectedTax = null;
                    TempTax = null;
                    CancelAddingTax();
                }
                else
                {
                    IsSettingsError = true;
                }
            }
            catch (Exception)
            {
                IsSettingsError = true;
            }
        }

        private void GoChangeTax()
        {
            if (SelectedTax != null)
            {
                TempTax = SelectedTax.Clone() as Tax;
                AccountantControl.StaticNavigate(TaxesAddingControl.GetInstance(this), this);
            }
        }

        private void RemoveTax()
        {
            if (SelectedTax != null)
            {
                if (MessageBox.Show((string)Application.Current.Resources["taxes_remove_quest"], (string)Application.Current.Resources["taxes_remove_text"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DB.Taxes.Remove(SelectedTax);
                    DB.SaveChanges();
                    SelectedTax = null;
                    SelectTaxes();
                }
            }
        }
        #endregion
        #endregion
    }
}
