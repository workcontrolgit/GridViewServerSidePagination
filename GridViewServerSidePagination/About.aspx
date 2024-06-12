<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="AspNetWebformSample.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
       <h2>Main GridView</h2>
       <p>This application shows a GridView displaying a list of user profiles with details such as Profile ID, Name, Email, and Mobile. The GridView also includes action buttons for editing and deleting each profile.</p>
       <h4>Key Features:</h4>
       <ul>
           <li><strong>Add Profile Button:</strong> Located at the top left, allows users to add a new profile.</li>
           <li><strong>Edit Button:</strong> Each row has an "Edit" button (in teal), which allows users to modify the details of the corresponding profile.</li>
           <li><strong>Delete Button:</strong> Each row has a "Delete" button (in red), which allows users to remove the corresponding profile from the list.</li>
           <li><strong>Pagination:</strong> At the bottom left, "Next" and "Last" buttons allow users to navigate through pages of profiles.</li>
           <li><strong>Page Size Dropdown:</strong> At the top right, a dropdown menu lets users select the number of profiles displayed per page.</li>
           <li><strong>Navigation Links:</strong> Links to "Profile", "About", and "Contact" pages are available at the top right.</li>
       </ul>
       
       <h2>Edit Profile Modal</h2>
       <p>This application shows a modal window for editing a user profile.</p>
       <h4>Key Features:</h4>
       <ul>
           <li><strong>Profile ID:</strong> Displayed but not editable.</li>
           <li><strong>Editable Fields:</strong> Name, Address, Email, Mobile, and Status fields can be edited.</li>
           <li><strong>Save Button:</strong> At the bottom right, the "Save" button saves the changes made to the profile.</li>
           <li><strong>Close Button:</strong> At the bottom right, the "Close" button closes the modal without saving changes.</li>
       </ul>

       <h2>CRUD Functions</h2>
       <ul>
           <li><strong>Create:</strong> The "Add Profile" button allows users to create a new profile by opening a form where details can be entered.</li>
           <li><strong>Read:</strong> The GridView displays the list of profiles, showing information for each profile.</li>
           <li><strong>Update:</strong> The "Edit" button opens a modal where the user can update profile details. Changes are saved by clicking the "Save" button.</li>
           <li><strong>Delete:</strong> The "Delete" button removes the corresponding profile from the list.</li>
       </ul>
<p>These functionalities are typical for managing user data in a web application, leveraging a user-friendly interface with Bootstrap 4 for consistent styling and responsive design.</p>
</asp:Content>
