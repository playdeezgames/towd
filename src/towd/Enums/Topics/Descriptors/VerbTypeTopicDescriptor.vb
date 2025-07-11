﻿Imports towd.business

Friend Class VerbTypeTopicDescriptor
    Implements ITopic
    Private ReadOnly VerbType As String

    Public Sub New(topic As String, verbType As String)
        Me.Topic = topic
        Me.VerbType = verbType
    End Sub

    Public ReadOnly Property Topic As String Implements ITopic.Topic

    Public ReadOnly Property Title As String Implements ITopic.Title
        Get
            Return VerbType.ToVerbTypeDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property Content As String Implements ITopic.Content
        Get
            Return VerbType.ToVerbTypeDescriptor.Description
        End Get
    End Property
End Class
