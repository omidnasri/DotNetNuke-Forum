'
' DotNetNuke� - http://www.dotnetnuke.com
' Copyright (c) 2002-2010
' by DotNetNuke Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'
Option Strict On
Option Explicit On 

Namespace DotNetNuke.Modules.Forum

#Region "ForumPreConfig"

	''' <summary>
	''' Runs only when a forum module is first placed on a page to set 
	''' configuration defaults and to create a new forum group and a new 
	''' default forum so user can use immediately
	''' </summary>
	''' <remarks>Only fires off when a new module instance is placed on a page.
	''' </remarks>
	''' <history>
	''' 	[cpaterra]	2/11/2006	Created
	''' </history>
	Public Class ForumPreConfig

#Region "Public Shared Methods"

		''' <summary>
		''' Creates the default forum configuration (module settings) for a new
		''' instance of the module.
		''' </summary>
		''' <param name="ModuleId">The moduleID of the module instance we are configuring.</param>
		''' <param name="PortalId">The portalID of the module isntance we are configuring.</param>
		''' <param name="UserId">The UserID of who has placed the module on a page.</param>
		''' <remarks>This adds module settings and creates a froum group and forum to allow users to start using module immediately after placing on a page. 
		''' It has later been useful for upgrade situations.
		''' </remarks>
		''' <history>
		''' </history>
		Public Shared Sub PreConfig(ByVal ModuleId As Integer, ByVal PortalId As Integer, ByVal UserId As Integer)
			Dim ctlModule As New DotNetNuke.Entities.Modules.ModuleController
			Dim _portalSettings As Portals.PortalSettings = Portals.PortalController.GetCurrentPortalSettings

			ctlModule.UpdateModuleSetting(ModuleId, "Name", "Forum")
			Dim mSourceDirectory As String = ApplicationPath & "/DesktopModules/Forum"
			ctlModule.UpdateModuleSetting(ModuleId, "ForumSkin", "Default")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableUserSkin", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableTimeZone", "True")
			' mail notifications
			ctlModule.UpdateModuleSetting(ModuleId, "MailNotification", "True")
			If Not _portalSettings.Email Is Nothing Then
				ctlModule.UpdateModuleSetting(ModuleId, "AutomatedEmailAddress", _portalSettings.Email)
			Else
				ctlModule.UpdateModuleSetting(ModuleId, "AutomatedEmailAddress", "forum@YOURDOMAIN.com")
			End If
			ctlModule.UpdateModuleSetting(ModuleId, "EnablePerForumFrom", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableListServer", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "ListServerFolder", "Forums/ListServer/")

			' New
			ctlModule.UpdateModuleSetting(ModuleId, "IconBarAsImages", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableAttachment", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "AggregatedForums", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "ThreadsPerPage", "10")
			ctlModule.UpdateModuleSetting(ModuleId, "PostsPerPage", "5")
			ctlModule.UpdateModuleSetting(ModuleId, "PopularThreadView", "200")
			ctlModule.UpdateModuleSetting(ModuleId, "PopularThreadReply", "10")
			' Rankings
			ctlModule.UpdateModuleSetting(ModuleId, "Ranking", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "FirstRankPosts", "1000")
			ctlModule.UpdateModuleSetting(ModuleId, "SecondRankPosts", "900")
			ctlModule.UpdateModuleSetting(ModuleId, "ThirdRankPosts", "800")
			ctlModule.UpdateModuleSetting(ModuleId, "FourthRankPosts", "700")
			ctlModule.UpdateModuleSetting(ModuleId, "FifthRankPosts", "600")
			ctlModule.UpdateModuleSetting(ModuleId, "SixthRankPosts", "500")
			ctlModule.UpdateModuleSetting(ModuleId, "SeventhRankPosts", "400")
			ctlModule.UpdateModuleSetting(ModuleId, "EigthRankPosts", "300")
			ctlModule.UpdateModuleSetting(ModuleId, "NinthRankPosts", "200")
			ctlModule.UpdateModuleSetting(ModuleId, "TenthRankPosts", "100")
			' RSS
			ctlModule.UpdateModuleSetting(ModuleId, "EnableRSS", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "RSSThreadsPerFeed", "20")
			ctlModule.UpdateModuleSetting(ModuleId, "RSSUpdateInterval", "30")
			' Bad Word Filter
			ctlModule.UpdateModuleSetting(ModuleId, "EnableBadWordFilter", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "FilterSubject", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableRatings", "True")
			' Rating
			ctlModule.UpdateModuleSetting(ModuleId, "RatingScale", "5")

			' Users online module integration
			Dim Enabled As Boolean
			If Entities.Host.Host.GetHostSettingsDictionary.ContainsKey("DisableUsersOnline") Then
				If Entities.Host.Host.GetHostSettingsDictionary("DisableUsersOnline").ToString = "Y" Then
					Enabled = False
				Else
					Enabled = True
				End If
			Else
				Enabled = False
			End If

			'Community
			ctlModule.UpdateModuleSetting(ModuleId, "EnableUsersOnline", Enabled.ToString)
			ctlModule.UpdateModuleSetting(ModuleId, "EnablePMSystem", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableMemberList", "False")

			' User Country displayed in posts area (next to subject, user alias)
			ctlModule.UpdateModuleSetting(ModuleId, "DisplayPosterLocation", "0")

			' Anon Attach
			ctlModule.UpdateModuleSetting(ModuleId, "AnonDownloads", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableThreadStatus", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "EnablePostAbuse", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "DisableHTMLPosting", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "ForumMemberName", "0")
			ctlModule.UpdateModuleSetting(ModuleId, "TrustNewUsers", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "ImageExtension", "png")

			ctlModule.UpdateModuleSetting(ModuleId, "EnableUserAvatar", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableUserAvatarPool", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "UserAvatarPoolPath", "Forums/PoolAvatar/")
			ctlModule.UpdateModuleSetting(ModuleId, "UserAvatarPath", "Forums/UserAvatar/")
			ctlModule.UpdateModuleSetting(ModuleId, "UserAvatarWidth", "128")
			ctlModule.UpdateModuleSetting(ModuleId, "UserAvatarHeight", "128")
			ctlModule.UpdateModuleSetting(ModuleId, "UserAvatarMaxSize", "64")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableSystemAvatar", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "SystemAvatarPath", "Forums/SystemAvatar/")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableRoleAvatar", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "RoleAvatarPath", "Forums/RoleAvatar/")
			ctlModule.UpdateModuleSetting(ModuleId, "EmailAddressDisplayName", _portalSettings.PortalName & " " & Localization.GetString("Forum", mSourceDirectory & "/App_LocalResources/SharedResources.resx"))
			ctlModule.UpdateModuleSetting(ModuleId, "PostEditWindow", "0")

			ctlModule.UpdateModuleSetting(ModuleId, "EnableEmoticons", "False")
			ctlModule.UpdateModuleSetting(ModuleId, "EmoticonMaxFileSize", "32")
			ctlModule.UpdateModuleSetting(ModuleId, "NoFollowLatestThreads", "True")
			ctlModule.UpdateModuleSetting(ModuleId, "EnableInlineAttachments", "False")

			' Prepare to retire treeview (vs. flatview)
			ctlModule.UpdateModuleSetting(ModuleId, "EnableTreeView", "False")

			SetupDefaultGroup(ModuleId, PortalId, UserId, mSourceDirectory)
		End Sub

		''' <summary>
		''' Adds a default forum to the default group created here as well
		''' </summary>
		''' <param name="ModuleId">The ModuleID of the module being configured.</param>
		''' <param name="PortalId">The PortalID of the module being configured.</param>
		''' <param name="UserId">The UserID of the person placing the module on a page.</param>
		''' <param name="mSourceDirectory">The module's path to it's files.</param>
		''' <remarks>Used to create a default group for a new module instance.
		''' </remarks>
		''' <history>
		''' </history>
		Public Shared Sub SetupDefaultGroup(ByVal ModuleId As Integer, ByVal PortalId As Integer, ByVal UserId As Integer, ByVal mSourceDirectory As String)
			Dim cntGroup As New GroupController
			Dim cntForum As New ForumController
			Dim newGroupName As String = Localization.GetString("DefaultForumGroupName", mSourceDirectory & "/App_LocalResources/SharedResources.resx")
			Dim newForumName As String = Localization.GetString("DefaultForumName", mSourceDirectory & "/App_LocalResources/SharedResources.resx")
			Dim GroupID As Integer
			Dim objUser As New Users.UserInfo
			Dim cntUser As New Entities.Users.UserController

			objUser = cntUser.GetUser(PortalId, UserId)
			Try
				GroupID = cntGroup.GroupAdd(newGroupName, PortalId, ModuleId, objUser.UserID)
				Dim objForum As New ForumInfo

				objForum.GroupID = GroupID
				objForum.Name = newForumName
				objForum.ForumType = ForumType.Normal
				objForum.ForumBehavior = ForumBehavior.PublicUnModerated
				objForum.IsActive = True
				objForum.IntegratedModuleID = 0
				objForum.IsIntegrated = False
				objForum.CreatedByUser = UserId
				objForum.ModuleID = ModuleId
				objForum.IntegratedObjects = String.Empty
				objForum.ParentId = 0
				' Email
				objForum.NotifyByDefault = False
				objForum.EmailStatusChange = False
				objForum.EmailEnableSSL = False
				objForum.EmailAuth = 0

				cntForum.ForumAdd(objForum)
			Catch ex As Exception
				LogException(ex)
			End Try

		End Sub

#End Region

	End Class

#End Region

End Namespace