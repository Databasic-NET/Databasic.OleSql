Imports System.Data.Odbc

Public Class SqlError
    Inherits Databasic.SqlError

	Public Property Source As String
	Public Property SQLState As String

	Public Sub New(oleDbSqlError As System.Data.OleDb.OleDbError)
		Me.Message = oleDbSqlError.Message
		Me.Code = oleDbSqlError.NativeError

		Me.Source = oleDbSqlError.Source
		Me.SQLState = oleDbSqlError.SQLState
	End Sub

End Class