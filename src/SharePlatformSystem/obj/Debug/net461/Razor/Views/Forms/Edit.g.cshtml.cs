#pragma checksum "G:\SharePlatformSystem\src\SharePlatformSystem\Views\Forms\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b973f8658ea3bf4adbad574e709cf06e8953edc2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Forms_Edit), @"mvc.1.0.view", @"/Views/Forms/Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Forms/Edit.cshtml", typeof(AspNetCore.Views_Forms_Edit))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b973f8658ea3bf4adbad574e709cf06e8953edc2", @"/Views/Forms/Edit.cshtml")]
    public class Views_Forms_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            DefineSection("header", async() => {
                BeginContext(18, 178, true);
                WriteLiteral("\r\n    <link rel=\"stylesheet\" href=\"/css/treetable.css\" />\r\n    <style>\r\n        .layui-btn + .layui-btn {\r\n            margin-left: inherit !important;\r\n        }\r\n    </style>\r\n");
                EndContext();
            }
            );
            BeginContext(199, 4962, true);
            WriteLiteral(@"

<form class=""layui-form layui-form-pane"" action="""" id=""formEdit"">
    <input type=""hidden"" name=""Id"" v-model=""Id"" />
    <input type=""hidden"" name=""Fields"" id=""Fields"" value=""0"">

    <div class=""layui-row"">
        <div class=""layui-tab layui-tab-brief"" lay-filter=""tab"" style=""margin: 0px"">
            <ul class=""layui-tab-title"">
                <li class=""layui-this"">基本信息</li>
                <li>表单设计</li>
            </ul>
            <div class=""layui-tab-content"" style=""height: 100px;"">
                <div class=""layui-tab-item layui-show"">
                    <div class=""layui-form-item"" pane>
                        <label class=""layui-form-label"">表单名称</label>
                        <div class=""layui-input-block"">
                            <input type=""text"" name=""Name"" v-model=""Name"" required lay-verify=""required""
                                   placeholder=""表单名称"" autocomplete=""off"" class=""layui-input"">
                        </div>
                    </div>

        ");
            WriteLiteral(@"            <div class=""layui-form-item"" pane>
                        <label class=""layui-form-label"">关联数据表名</label>
                        <div class=""layui-input-block"">
                            <input type=""text"" name=""DbName"" v-model=""DbName"" required 
                                   placeholder=""关联数据表名称"" autocomplete=""off"" class=""layui-input"">
                        </div>
                    </div>

                    <div class=""layui-form-item"" pane>
                        <label class=""layui-form-label"">排序码</label>
                        <div class=""layui-input-block"">
                            <input type=""text"" name=""SortCode"" v-model=""SortCode"" class=""layui-input"">
                        </div>
                    </div>

                    <div class=""layui-form-item"" pane>
                        <label class=""layui-form-label"">备注</label>
                        <div class=""layui-input-block"">
                            <textarea  name=""Description"" v-model=""De");
            WriteLiteral(@"scription""
                                   placeholder=""备注"" class=""layui-textarea""></textarea>
                        </div>
                    </div>

                </div>
                <div class=""layui-tab-item"">
                    <div class=""layui-row"">
                        <blockquote class=""layui-elem-quote"">
                            提醒：单选框和复选框，如：{|-选项-|}两边边界是防止误删除控件，程序会把它们替换为空，请不要手动删除！
                        </blockquote>
                    </div>
                    <div class=""layui-row"">
                        <div class=""layui-col-xs2 layui-btn-container"">
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('text');"">文本框</button>
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('textarea');"">多行文本</button>
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('select')");
            WriteLiteral(@";"">下拉菜单</button>
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('radios');"">单选框</button>
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('checkboxs');"">复选框</button>
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('macros');"">宏控件</button>
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('progressbar');"">进度条</button>
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('qrcode');"">二维码</button>
                            <button class=""layui-btn layui-btn-primary layui-btn-fluid"" onclick=""leipiFormDesign.exec('listctrl');"">列表控件</button>
                        </div>
                        <div class=""layui-col-xs10"">
                            <script id=""myFor");
            WriteLiteral(@"mDesign"" type=""text/plain"" style=""width: 100%;"">
                            </script>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <input type=""hidden"" lay-submit id=""btnSubmit"" lay-filter=""formSubmit"" />
</form>

<script type=""text/javascript"" charset=""utf-8"" src=""/js/ueditor/ueditor.config.js?2023""></script>
<script type=""text/javascript"" charset=""utf-8"" src=""/js/ueditor/ueditor.all.js?2023""> </script>
<script type=""text/javascript"" charset=""utf-8"" src=""/js/ueditor/lang/zh-cn/zh-cn.js?2023""></script>
<script type=""text/javascript"" charset=""utf-8"" src=""/js/ueditor/formdesign/leipi.formdesign.v4.js?2023""></script>

<script type=""text/javascript"" src=""/layui/layui.js""></script>
<script type=""text/javascript"" src=""/userJs/formEdit.js?v4""></script>


");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
