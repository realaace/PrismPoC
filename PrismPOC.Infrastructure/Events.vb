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

Public Class ExitAppEvent
    Inherits CompositePresentationEvent(Of String)
End Class

Public Class SelectedItemChangedEvent
    Inherits CompositePresentationEvent(Of Object)
End Class

Public Class CreateViewEvent
    Inherits CompositePresentationEvent(Of String)
End Class

Public Class ViewCreatedEvent
    Inherits CompositePresentationEvent(Of String)
End Class

Public Class SaveLayoutWithNameEvent
    Inherits CompositePresentationEvent(Of String)
End Class
