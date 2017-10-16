Imports System.Data.Common
Imports System.Data.OleDb

Public Class Connection
	Inherits Databasic.Connection

	Public Overrides ReadOnly Property Provider As DbConnection
		Get
			Return Me._provider
		End Get
	End Property
	Private _provider As OleDbConnection

	Public Overrides ReadOnly Property ProviderResource As System.Type = GetType(ProviderResource)

	Public Overrides ReadOnly Property ClientName As String = "System.Data.OleDb"

	Public Overrides ReadOnly Property Statement As System.Type = GetType(Statement)

	Public Overrides Sub Open(dsn As String)
		Me._provider = New OleDbConnection(dsn)
		Me._provider.Open()
		AddHandler Me._provider.InfoMessage, AddressOf Connection.errorHandler
	End Sub

	Protected Shared Sub errorHandler(sender As Object, args As OleDbInfoMessageEventArgs)
		Dim sqlErrors As Databasic.SqlErrorsCollection = New SqlErrorsCollection()
		For index = 0 To args.Errors.Count - 1
			sqlErrors.Add(New Databasic.OleSql.SqlError(args.Errors(index)))
		Next
		Databasic.Events.RaiseError(sqlErrors)
	End Sub

	Protected Overrides Function createAndBeginTransaction(Optional transactionName As String = "", Optional isolationLevel As IsolationLevel = IsolationLevel.Unspecified) As Databasic.Transaction
		Me.OpenedTransaction = New Transaction() With {
			.ConnectionWrapper = Me,
			.Instance = Me._provider.BeginTransaction(isolationLevel)
		}
		Return Me.OpenedTransaction
	End Function

End Class
