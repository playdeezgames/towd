Imports towd.business

Friend Class VerbTypeTopicDescriptor
    Implements ITopic
    Sub New(topic As Topic, verbType As VerbType)
        Me.Topic = topic
        Me.VerbType = verbType
    End Sub
    Private ReadOnly Property VerbType As VerbType

    Public ReadOnly Property Topic As Topic Implements ITopic.Topic

    Public ReadOnly Property Title As String Implements ITopic.Title
        Get
            Return VerbType.ToDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property Content As String Implements ITopic.Content
        Get
            Return VerbType.ToDescriptor.Description
        End Get
    End Property
End Class
