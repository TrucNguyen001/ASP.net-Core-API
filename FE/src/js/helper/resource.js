if (!localStorage.getItem("language")) {
  localStorage.setItem("language", "VN");
}
const language = localStorage.getItem("language");
const message = {
  VN: {
    ErrorEmployee: {
      Title: "Thông tin không hợp lệ ?",
      ContentCode: "Mã nhân viên không được bỏ trống",
      ContentFullName: "Họ tên không được bỏ trống",
      ContentEmail: "Email không đúng định dạng",
      ContentBirth: "Ngày sinh không được lớn hơn ngày hiên tại",
      ContentIdentityCard: "Ngày cấp không được lớn hơn ngày hiên tại",
      ContentDepartment: "Phòng ban không được phép bỏ trống",
    },
    ConfirmEmployee: {
      Title: "Cất giữ liệu ?",
      Content: "Cất dữ liệu đang thiết lập để có thể sử dụng lại lần tới",
    },
    ConfirmDeleteEmployee: {
      Title: "Xoá bản ghi ?",
      ContentDelete: "Bạn có chắc chắn muốn xóa nhân viên",
      ContentDeleteAll:
        "Bạn có thực sự muốn xoá những nhân viên đã chọn không?",
    },
    ConfirmImportEmployee: {
      Title: "Thông tin dữ liệu",
      ContentSuccess: "Số lượng bản ghi thành công",
      ContentFail: "Số lượng bản ghi thất bại",
    },
    ConfirmImportFileError: {
      Title: "File không đúng định dạng?",
      Content:
        "File chọn không đúng định dạng [.xls, .xlsx] vui lòng kiểm tra lại.",
    },
    ConfirmClose: {
      Title: "Cất dữ liệu?",
      Content: "Dữ liệu bị thay đổi. Bạn có muốn cất không?",
    },
    ContentSuccess: {
      LoadDataSuccess: "Tải dữ liệu thành công",
      SaveDataSuccess: " Cất dữ liệu thành công",
      UpdateDataSuccess: " Sửa dữ liệu thành công",
      DeleteDataSuccess: " Nhân viên đã bị xoá",
    },
    Gender: {
      0: "Nam",
      1: "Nữ",
      2: "Khác",
    },
    Question: {
      Content: "không?",
    },
    ErrorLogin: {
      AccountNotEmpty: "Tên đăng nhập không được để trống",
      PasswordNotEmpty: "Mật khẩu không được bỏ trống",
      Login: "Thông tin tài khoản hoặc mật khẩu không chính xác",
    },
    NotFoundFile: {
      Title: "File không tồn tại",
      Content: "Nhập file trước khi chuyển sang bước tiếp theo",
    },
    ColumnTable: {
      EmployeeCode: "Mã nhân viên",
      EmployeeName: "Tên nhân viên",
      Gender: "Giới tính",
      DateOfBirth: "Ngày sinh",
      IdentificationCard: "Số CMND",
      PositionName: "Chức danh",
      DepartmentName: "Tên đơn vị",
      BankAccount: "Tài khoản ngân hàng",
      BankName: "Tên ngân hàng",
      BankAddress: "Chi nhánh TK ngân hàng",
      Status: "Tình trạng",
      Feature: "Chức năng",
      PhoneNumber: "Số điện thoại",
      TitleIdentificationCard: "Số chứng minh nhân dân",
      TitleBankAddress: "Chi nhánh tài khoản ngân hàng",
    },
    DisConnect: {
      Content: "Không thể kết nối với bất kỳ máy chủ MySQL nào được chỉ định",
    },
    MessError: {
      Content: "Có lỗi xảy ra hãy liên hệ với MISA để được hỗ trợ",
    },
    ErrorData: {
      Title: "Dữ liệu không hợp lệ",
    },
    ErrorNetwork: {
      Title: "Lỗi kết nối",
      Content: "Không thể kết nối đến máy chủ, vui lòng quay lại sau",
    },
    ErrorDate: {
      NotGreaterThanCurrentDate: "không được lớn hơn ngày hiện tại",
      NotValid: "không hợp lệ",
    },
    MCombobox: {
      Select: "- Chọn giá trị -",
    },
    MDialog: {
      Destroy: "Huỷ",
      No: "Không",
      Yes: "Đồng ý",
      Store: "Cất",
      YesAndSave: "Cất và thêm",
    },
    MInput: {
      NotEmpty: " không được phép bỏ trống",
      NotFind: " không tồn tại trong hệ thống",
      Branch: "Chi nhánh",
      BankName: "Tên ngân hàng",
      BankAccount: "Tài khoản ngân hàng",
      Email: "Email",
      LandlinePhone: "Điện thoại cố định",
      PhoneNumber: "Điện thoại di động",
      Address: "Địa chỉ",
      PlaceIdentificationCard: "Nơi cấp",
      DateOfIdentityCard: "Ngày cấp",
      IdentificationCard: "Số CMND",
      Gender: "Giới tính",
      DateOfBirth: "Ngày sinh",
      Position: "Chức vụ",
      Department: "Phòng ban",
      FullName: "Tên",
      Code: "Mã",
    },
    MPagination: {
      Record: "Bản ghi/Trang",
      Sum: "Tổng: ",
      ToTalRecord: "bản ghi",
    },
    MHeader: {
      NameCompany: "CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC",
      Name: "KẾ TOÁN",
      Login: "Đăng nhập",
      Logout: "Đăng xuất",
    },
    MNavBar: {
      Employee: "Nhân viên",
      Overview: "Tổng quan",
      Cash: "Tiền mặt",
      Deposits: "Tiền gửi",
      Shop: "Mua hàng",
      ManageInvoices: "Quản lý hoá đơn",
      WareHouse: "Kho",
      Tools: "Công cụ dụng cụ",
      FixedAssets: "Tài sản cố định",
      Tax: "Thuế",
      Price: "Giá thành",
      Summary: "Tổng hợp",
      Budget: "Ngân sách",
      Report: "Báo cáo",
      FinancialAnalysis: "Phân tích tài chính",
    },
    MDetailEmployee: {
      InfoEmployee: "Thông tin nhân viên",
      IsCustomer: "Là khách hàng",
      IsSupplier: "Là nhà cung cấp",
    },
    MEmployeeList: {
      DeleteAll: "Xoá tất cả",
      Selected: "Đã chọn tất cả: ",
      UnSelect: "Bỏ chọn",
      ManagerEmployee: "Quản lý nhân viên",
      AddEmployee: "Thêm mới nhân viên",
      BatchExecution: "Thực hiện hàng loạt",
      Delete: "Xoá",
      Update: "Sửa",
      Replication: "Nhân bản",
      Stop: "Ngừng sử dụng",
      Search: "Tìm theo mã, tên nhân viên",
    },
    MImportFile: {
      Title: "Nhập khẩu nhân viên",
      StepOne: "Bước 1: Chọn tệp nguồn",
      StepTwo: "Bước 2: Kiểm tra dữ liệu",
      StepThree: "Bước 3: Kết quả nhập khẩu",
      TitleStepOne: "1. Chọn tệp nguồn",
      TitleStepTwo: "2. Kiểm tra dữ liệu",
      TitleStepThree: "3. Kết quả nhập khẩu",
      SelectFile:
        "Chọn dữ liệu nhân viên đã chuẩn bị để nhập khẩu vào phần mềm",
      HaveFile:
        "Chưa có tệp mẫu để chuẩn bị dữ liệu ? Tải tệp excel mẫu mà phần mềm cung cấp để chuẩn bị dữ liệu nhập khẩu.",
      Here: "Tại đây",
      Help: "Giúp",
      Back: "Quay lại",
      Continue: "Tiếp tục",
      Cancel: "Huỷ bỏ",
      RowValid: "Dòng hợp lệ",
      RowNoValid: "Dòng không hợp lệ",
      DownloadFileFalse:
        "Tải về tập tin chứa các dòng nhập khẩu không thành công",
      ResultImport: "Kết quả nhập khẩu",
      DownloadFile: "Tải về tập tin chứa kết quả nhập khẩu",
      RowSuccess: "+ Số dòng nhập khẩu thành công: ",
      RowError: "+ Số dòng nhập khẩu không thành công: ",
      Close: "Đóng",
      ImportNoSuccess: {
        Content: "Không có bản ghi nào thành công. Xin vui lòng thử lại sau",
        Title: "Thêm không thành công",
      },
      Select: "Chọn",
      ToastError: {
        Content: "Không có bản ghi nào thành công",
      },
    },
    MLogin: {
      VN: "VN",
      Language: "Việt Nam",
      ForgetPassword: "Quên mật khẩu?",
      Login: "Đăng nhập",
      LoginWith: "Hoặc đăng nhập với",
      CopyRight: "Copyright © 2012 - 2024 MISA JSC",
      Account: "Số điện thoại/email",
      Password: "Mật khẩu",
      PasswordNew: "Mật khẩu mới",
      PasswordNewConfirm: "Xác nhận mật khẩu mới",
    },
    DialogUpdatePasswordSuccess: {
      Title: "Cất dữ liệu",
      Content: "Thay đổi dữ liệu thành công",
    },
    MForgetPassword: {
      ErrorValidPassword:
        "Mật khẩu phải có ít nhất 8 kí tự bao gồm kí tự chữ hoa, chữ thường và chữ số",
      ErrorFormatEmail: "Email không đúng định dạng",
      ErrorEmailEmpty: "Email không được để trống",
      ErrorCode: "Nhập mã xác thưc",
      NewPasswordNull: "Nhập mật khẩu",
      ConfirmNewPasswordNull: "Nhập xác nhận mật khẩu",
      PasswordDifferent: "Mật khẩu mới và xác nhận mật khẩu không trùng nhau",
      RecoverPassword: "Lấy lại mật khẩu",
      EnterEmail: "Nhập email của bạn để nhận mã xác thực lấy lại",
      Password: "mật khẩu",
      BackLogin: "Quay lại đăng nhập",
      UsePhoneNumber: "Sử dụng số điện thoại",
      Help: "Trợ giúp | Tiếng Việt",
      CodeAuth: "Mã xác thực đã được gửi đến email",
      CheckEmail: ". Vui lòng kiểm tra email và",
      EnterInput: "nhập chính xác mã vào ô dưới",
      Continue: "Tiếp tục",
      CreatePassword: "Tạo mật khẩu",
      CreateNewPassword: "Tạo mật khẩu mới",
      FormatLetters: "Nhập mật khẩu có tối thiểu 8 ký tự bao gồm số,",
      UpperLower: "chữ hoa, chữ thường",
    },
  },
  EN: {
    ErrorEmployee: {
      Title: "Invalid information?",
      ContentCode: "Employee code cannot be empty",
      ContentFullName: "Full name cannot be left blank",
      ContentEmail: "Email is not in the correct format",
      ContentBirth: "The date of birth not than the current date",
      ContentIdentityCard: "Issue date not than the current date",
      ContentDepartment: "Departments are not allowed to be empty",
    },
    ConfirmEmployee: {
      Title: "Store data?",
      Content:
        "Save the data you're setting up so you can use it again next time",
    },
    ConfirmDeleteEmployee: {
      Title: "Delete record?",
      ContentDelete: "Are you want to delete the employee",
      ContentDeleteAll: "Do you really want to delete the selected employees?",
    },
    ConfirmImportEmployee: {
      Title: "Data information",
      ContentSuccess: "Number of successful records",
      ContentFail: "Number of failed records",
    },
    ConfirmImportFileError: {
      Title: "File is not in the correct format?",
      Content:
        "The selected file is not in the correct format [.xls, .xlsx] please check again.",
    },
    ConfirmClose: {
      Title: "Store data?",
      Content: "Data has been changed. Do you want to save it?",
    },
    ContentSuccess: {
      LoadDataSuccess: "Load data successfully",
      SaveDataSuccess: " Save data successfully",
      DeleteDataSuccess: " Delete data successfully",
    },
    Gender: {
      0: "Male",
      1: "Female",
      2: "Other",
    },
    Question: {
      Content: "no?",
    },
    ErrorLogin: {
      AccountNotEmpty: "Username cannot be empty",
      PasswordNotEmpty: "Password cannot be empty",
      Login: "Incorrect account or password information",
    },
    NotFoundFile: {
      Title: "File does not exist",
      Content: "Import files before moving to the next step",
    },
    ColumnTable: {
      EmployeeCode: "Employee code",
      EmployeeName: "Employee name",
      Gender: "Gender",
      DateOfBirth: "Date of birth",
      IdentificationCard: "Id cart number",
      PositionName: "Position name",
      DepartmentName: "Department name",
      BankAccount: "Bank account",
      BankName: "Bank name",
      BankAddress: "Bank account branch",
      Status: "Status",
      Feature: "Function",
      PhoneNumber: "Phone number",
    },
    DisConnect: {
      Content: "Unable to connect to any of the specified MySQL hosts",
    },
    MessError: {
      Content: "If an error occurs, please contact MISA for support",
    },
    ErrorData: {
      Title: "Invalid data",
    },
    ErrorNetwork: {
      Title: "Connection errors",
      Content: "Unable to connect to the server, please try again later",
    },
    ErrorDate: {
      NotGreaterThanCurrentDate: "not than the current date",
      NotValid: "not valid",
    },
    MCombobox: {
      Select: "- Select value -",
    },
    MDialog: {
      Destroy: "Cancel",
      No: "No",
      Yes: "Agree",
      Store: "Store",
      YesAndSave: "Save and add",
    },
    MInput: {
      NotEmpty: "cannot be left blank",
      NotFind: " does not exist in the system",
      Branch: "Branch",
      BankName: "Bank name",
      BankAccount: "Bank account",
      Email: "Email",
      LandlinePhone: "Landline phone",
      PhoneNumber: "Mobile phone",
      Address: "Address",
      PlaceIdentificationCard: "Place of issue",
      DateOfIdentityCard: "Date issued",
      IdentificationCard: "ID card number",
      Gender: "Gender",
      DateOfBirth: "Date of birth",
      Position: "Position",
      Department: "Department",
      FullName: "Name",
      Code: "Code",
    },
    MPagination: {
      Record: "Record/Page",
      Sum: "Total: ",
      ToTalRecord: "record",
    },
    MHeader: {
      NameCompany: "QUI PHUC PRODUCTION - TRADING - SERVICES COMPANY LIMITED",
      Name: "ACCOUNT",
      Login: "Login",
      Logout: "Log out",
    },
    MNavBar: {
      Employee: "Employee",
      Overview: "Overview",
      Cash: "Cash",
      Deposits: "Deposits",
      Shop: "Buy",
      ManageInvoices: "Manage invoices",
      WareHouse: "Warehouse",
      Tools: "Tools",
      FixedAssets: "Fixed assets",
      Tax: "Tax",
      Price: "Cost",
      Summary: "Synthesis",
      Budget: "Budget",
      Report: "Report",
      FinancialAnalysis: "Financial Analysis",
    },
    MDetailEmployee: {
      InfoEmployee: "Employee information",
      IsCustomer: "Is a customer",
      IsSupplier: "Is supplier",
    },
    MEmployeeList: {
      DeleteAll: "Delete all",
      Selected: "Selected all: ",
      UnSelect: "Deselect",
      ManagerEmployee: "Manage employees",
      AddEmployee: "Add new employee",
      BatchExecution: "Batch execution",
      Delete: "Delete",
      Update: "Edit",
      Replication: "Clone",
      Stop: "Stop using",
      Search: "Search by code, name",
    },
    MImportFile: {
      Title: "Import employees",
      StepOne: "Step 1: Select the source file",
      StepTwo: "Step 2: Check the data",
      StepThree: "Step 3: Import result",
      TitleStepOne: "1. Select the source file",
      TitleStepTwo: "2. Check the data",
      TitleStepThree: "3. Import result",
      SelectFile:
        "Select the prepared employee data to import into the software",
      HaveFile:
        "Do not have a sample file to prepare import data? Download the sample excel file provided by the software to prepare the import data.",
      Here: "Here",
      Help: "Help",
      Back: "Back",
      Continue: "Continue",
      Cancel: "Cancel",
      RowValid: " Valid row",
      RowNoValid: " No valid line",
      DownloadFileFalse: "Download file containing failed import lines",
      ResultImport: "Import result",
      DownloadFile: "Download file containing import results",
      RowSuccess: "+ Number of successful import lines: ",
      RowError: "+ Number of unsuccessful import lines: ",
      Close: "Close",
      ImportNoSuccess: {
        Content: "No successful recordings. Please try again later",
        Title: "Adding failed",
      },
      Select: "Select",
      ToastError: {
        Content: "None of the recordings were successful",
      },
    },
    MLogin: {
      VN: "EN",
      Language: "English",
      ForgetPassword: "Forgot password?",
      Login: "Login",
      LoginWith: "Or login with",
      CopyRight: "Copyright © 2012 - 2024 MISA JSC",
      Account: "Phone number/email",
      Password: "Password",
      PasswordNew: "New password",
      PasswordNewConfirm: "Confirm new password",
    },
    DialogUpdatePasswordSuccess: {
      Title: "Save data",
      Content: "Successfully changed your data",
    },
    MForgetPassword: {
      ErrorValidPassword:
        "The password must be at least 8 characters long including uppercase letters, lowercase letters, and numbers",
      ErrorFormatEmail: "Email is not in the correct format",
      ErrorEmailEmpty: "Email cannot be left blank",
      ErrorCode: "Enter Auth Code",
      NewPasswordNull: "Enter password",
      ConfirmNewPasswordNull: "Enter confirm password",
      PasswordDifferent: "New password and confirm password do not match",
      RecoverPassword: "Recover password",
      EnterEmail: "Enter your email to receive the recovery verification code",
      Password: "Password",
      BackLogin: "Back to login",
      UsePhoneNumber: "Use phone number",
      Help: "Help | Vietnamese",
      CodeAuth: "Verification code has been sent to email",
      CheckEmail: ". Please check your email and",
      EnterInput: "enter the correct code into the box below",
      Continue: "Continue",
      CreatePassword: "Create password",
      CreateNewPassword: "Create new password",
      FormatLetters: "Enter a password minimum of 8 characters, numbers,",
      UpperLower: "uppercase letters, lowercase letters",
    },
  },
};
export default message[language];
