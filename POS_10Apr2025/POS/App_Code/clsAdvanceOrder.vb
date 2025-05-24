Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class controller_clsAdvanceOrder
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Private Variables"
    Private _Cmp_Id As Integer
    Private _Name As String
    Private _Target_date As DateTime
    Private _Item_Id As Integer
    Private _Quantity As Integer
    Private _Login_id As Integer
    Private _Ip_address As String

    Private _Advance_Tran_id As Integer
    Private _Tran_id As Integer
    Private _Unit_id As Integer
    Private _Is_Active As Boolean
    Private _Tran_Type As String
    Private objclsAdvanceOrder As clsAdvanceOrder
#End Region

#Region "Public Property"
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public Property Target_date() As DateTime
        Get
            Return _Target_date

        End Get
        Set(ByVal value As DateTime)
            _Target_date = value
        End Set
    End Property
    Public Property Cmp_Id() As Integer
        Get
            Return _Cmp_Id
        End Get
        Set(ByVal value As Integer)
            _Cmp_Id = value
        End Set
    End Property
    Public Property Item_Id() As Integer
        Get
            Return _Item_Id
        End Get
        Set(ByVal value As Integer)
            _Item_Id = value
        End Set
    End Property
    Public Property Quantity() As Integer
        Get
            Return _Quantity
        End Get
        Set(ByVal value As Integer)
            _Quantity = value
        End Set
    End Property
    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
        End Set
    End Property

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
        End Set
    End Property

    Public Property Advance_Tran_id() As String
        Get
            Return _Advance_Tran_id
        End Get
        Set(ByVal value As String)
            _Advance_Tran_id = value
        End Set
    End Property
    Public Property Tran_id() As Integer
        Get
            Return _Tran_id
        End Get
        Set(ByVal value As Integer)
            _Tran_id = value
        End Set
    End Property
    Public Property Unit_id() As Integer
        Get
            Return _Unit_id
        End Get
        Set(ByVal value As Integer)
            _Unit_id = value
        End Set
    End Property
    Public Property Is_Active() As Boolean
        Get
            Return _Is_Active
        End Get
        Set(ByVal value As Boolean)
            _Is_Active = value
        End Set
    End Property
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            objclsAdvanceOrder = New clsAdvanceOrder()
            objclsAdvanceOrder.Cmp_Id = Cmp_Id
            objclsAdvanceOrder.Name = Name
            objclsAdvanceOrder.Target_date = Convert.ToDateTime(Target_date)
            objclsAdvanceOrder.Item_Id = Item_Id
            objclsAdvanceOrder.Quantity = Quantity
            objclsAdvanceOrder.Login_id = Login_id
            objclsAdvanceOrder.Ip_address = Ip_address
            objclsAdvanceOrder.Advance_Tran_id = Advance_Tran_id
            objclsAdvanceOrder.Tran_id = Tran_id
            objclsAdvanceOrder.Unit_id = Unit_id
            objclsAdvanceOrder.Is_Active = Is_Active
            objclsAdvanceOrder.Tran_Type = Tran_Type
            If objclsAdvanceOrder.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function selectAll() As DataTable
        objclsAdvanceOrder = New clsAdvanceOrder()
        objclsAdvanceOrder.Cmp_Id = Cmp_Id
        objclsAdvanceOrder.Tran_Type = Tran_Type
        Dim dt As DataTable
        dt = objclsAdvanceOrder.selectAll()
        Return dt
    End Function
    Public Function DeleteAll() As Boolean
        objclsAdvanceOrder = New clsAdvanceOrder()
        objclsAdvanceOrder.Cmp_Id = Cmp_Id
        objclsAdvanceOrder.Advance_Tran_id = Advance_Tran_id
        objclsAdvanceOrder.Tran_Type = Tran_Type
        If objclsAdvanceOrder.DeleteAll() Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Update() As Boolean
        objclsAdvanceOrder = New clsAdvanceOrder()
        objclsAdvanceOrder.Cmp_Id = Cmp_Id
        objclsAdvanceOrder.Name = Name
        objclsAdvanceOrder.Target_date = Convert.ToDateTime(Target_date)
        objclsAdvanceOrder.Item_Id = Item_Id
        objclsAdvanceOrder.Quantity = Quantity
        objclsAdvanceOrder.Advance_Tran_id = Advance_Tran_id
        objclsAdvanceOrder.Tran_id = Tran_id
        objclsAdvanceOrder.Unit_id = Unit_id
        objclsAdvanceOrder.Is_Active = Is_Active

        objclsAdvanceOrder.Advance_Tran_id = Advance_Tran_id
        objclsAdvanceOrder.Tran_Type = Tran_Type
        If objclsAdvanceOrder.Update() Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

Public Class clsAdvanceOrder
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Private Variables"
    Private _Name As String
    Private _Target_date As DateTime
    Private _Item_Id As Integer
    Private _Quantity As Integer
    Private _Login_id As Integer
    Private _Ip_address As String
    Private _Cmp_Id As Integer
    Private _Advance_Tran_id As Integer
    Private _Tran_id As Integer
    Private _Unit_id As Integer
    Private _Is_Active As Boolean
    Private _Tran_Type As String
#End Region

#Region "Public Property"
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public Property Cmp_Id() As Integer
        Get
            Return _Cmp_Id
        End Get
        Set(ByVal value As Integer)
            _Cmp_Id = value
        End Set
    End Property
    Public Property Target_date() As DateTime
        Get
            Return _Target_date

        End Get
        Set(ByVal value As DateTime)
            _Target_date = value
        End Set
    End Property
    Public Property Item_Id() As Integer
        Get
            Return _Item_Id
        End Get
        Set(ByVal value As Integer)
            _Item_Id = value
        End Set
    End Property
    Public Property Quantity() As Integer
        Get
            Return _Quantity
        End Get
        Set(ByVal value As Integer)
            _Quantity = value
        End Set
    End Property
    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
        End Set
    End Property

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
        End Set
    End Property

    Public Property Advance_Tran_id() As String
        Get
            Return _Advance_Tran_id
        End Get
        Set(ByVal value As String)
            _Advance_Tran_id = value
        End Set
    End Property
    Public Property Tran_id() As Integer
        Get
            Return _Tran_id
        End Get
        Set(ByVal value As Integer)
            _Tran_id = value
        End Set
    End Property
    Public Property Unit_id() As Integer
        Get
            Return _Unit_id
        End Get
        Set(ByVal value As Integer)
            _Unit_id = value
        End Set
    End Property
    Public Property Is_Active() As Boolean
        Get
            Return _Is_Active
        End Get
        Set(ByVal value As Boolean)
            _Is_Active = value
        End Set
    End Property
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
#End Region
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Cmp_id", Cmp_Id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Target_date", Target_date, SqlDbType.DateTime)
            oColSqlparram.Add("@Item_id", Item_Id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Quantity", Quantity, Data.SqlDbType.Int)
            oColSqlparram.Add("@Login_id", Login_id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", Login_id)
            oColSqlparram.Add("@Advance_Tran_id", Advance_Tran_id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Unit_id", Unit_id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Is_Active", Is_Active, Data.SqlDbType.Bit)
            oColSqlparram.Add("@Tran_Type", Tran_Type, Data.SqlDbType.VarChar)

            oClsDal.ExecStoredProcedure("P_Advance_Order_Generation", oColSqlparram)

            Return True
        Catch ex As Exception

            Throw New Exception(ex.Message)

        End Try
    End Function

    Public Function selectAll() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Cmp_id", Cmp_Id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", Tran_Type, Data.SqlDbType.VarChar)
            Dim dt As DataTable
            dt = oClsDal.GetdataTableSp("P_Advance_Order_Generation", oColSqlparram)
            Return dt
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function DeleteAll() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Cmp_id", Cmp_Id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Advance_Tran_id", Advance_Tran_id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", Tran_Type, Data.SqlDbType.VarChar)
            oClsDal.ExecStoredProcedure("P_Advance_Order_Generation", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Cmp_id", Cmp_Id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Target_date", Target_date, SqlDbType.DateTime)
            oColSqlparram.Add("@Item_id", Item_Id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Quantity", Quantity, Data.SqlDbType.Int)
            oColSqlparram.Add("@Login_id", Login_id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", Login_id)
            oColSqlparram.Add("@Advance_Tran_id", Advance_Tran_id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Unit_id", Unit_id, Data.SqlDbType.Int)
            oColSqlparram.Add("@Is_Active", Is_Active, Data.SqlDbType.Bit)
            oColSqlparram.Add("@Tran_Type", Tran_Type, Data.SqlDbType.VarChar)

            oClsDal.ExecStoredProcedure("P_Advance_Order_Generation", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
