Imports Microsoft.Practices.Prism.Events
Public Class TransactionButtonEvent
    Inherits CompositePresentationEvent(Of String)
End Class

Public Class SaveLayoutEvent
    Inherits CompositePresentationEvent(Of String)
End Class

Public Class LoadLayoutEvent
    Inherits CompositePresentationEvent(Of String)
End Class
