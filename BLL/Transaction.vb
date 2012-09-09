Public Class Transaction

#Region "Fields"

    Dim _dealer As String
    Dim _branch As String
    Dim _rep As String

#End Region 'Fields

#Region "Properties"

    Public Property Id As Integer

    Public Property Dealer As String
        Get
            Return _dealer
        End Get
        Set(value As String)
            _dealer = value
        End Set
    End Property

    Public Property Branch As String
        Get
            Return _branch
        End Get
        Set(value As String)
            _branch = value
        End Set
    End Property
    Public Property Rep As String
        Get
            Return _rep
        End Get
        Set(value As String)
            _rep = value
        End Set
    End Property

    Public Property Series As Integer

#End Region

    Public Sub New(ByVal id As Integer, ByVal dealer As String, ByVal branch As String, ByVal rep As String, ByVal series As Integer)
        _Id = id
        _dealer = dealer
        _branch = branch
        _rep = rep
        _Series = series

    End Sub

    Public Shared Function GetTransactions() As List(Of Transaction)
        Dim trans As New List(Of Transaction)
        trans.Add(New Transaction(1, "Dealer 1", "Branch 1", "Rep 1", 1))
        trans.Add(New Transaction(2, "Dealer 1", "Branch 1", "Rep 1", 2))
        trans.Add(New Transaction(3, "Dealer 2", "Branch 2", "Rep 2", 1))
        trans.Add(New Transaction(4, "Dealer 3", "Branch 3", "Rep 3", 3))
        trans.Add(New Transaction(5, "Dealer 3", "Branch 4", "Rep 5", 4))
        trans.Add(New Transaction(6, "Dealer 4", "Branch 1", "Rep 1", 1))

        Return trans
    End Function

End Class
