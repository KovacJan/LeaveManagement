#pragma checksum "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c37d72bb2713800bc962961aaac2c8ecbfd816d8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LeaveAllocation_ListEmployees), @"mvc.1.0.view", @"/Views/LeaveAllocation/ListEmployees.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\_ViewImports.cshtml"
using leave_management;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\_ViewImports.cshtml"
using leave_management.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c37d72bb2713800bc962961aaac2c8ecbfd816d8", @"/Views/LeaveAllocation/ListEmployees.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00e77ddfc85c045f6de54f7f6f5b70fff5cde1b1", @"/Views/_ViewImports.cshtml")]
    public class Views_LeaveAllocation_ListEmployees : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<leave_management.Models.EmployeeVM>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
  
    ViewData["Title"] = "ListEmployees";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>ListEmployees</h1>\r\n\r\n<table id=\"tblData\" class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 14 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
           Write(Html.DisplayNameFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 17 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
           Write(Html.DisplayNameFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 20 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
           Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>            \r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 26 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 29 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
           Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>            \r\n            <td>\r\n                ");
#nullable restore
#line 32 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
           Write(Html.DisplayFor(modelItem => item.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 35 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
           Write(Html.DisplayFor(modelItem => item.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>            \r\n            <td>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c37d72bb2713800bc962961aaac2c8ecbfd816d86818", async() => {
                WriteLiteral("\r\n                    <i class=\"fas fa-search\"></i> Details\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 38 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
                                                                          WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 43 "C:\Users\jan.kovac\source\repos\leave-management\leave-management\Views\LeaveAllocation\ListEmployees.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<leave_management.Models.EmployeeVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
