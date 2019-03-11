using System;

namespace SharePlatformSystem.Framework.Models
{
    /// <summary>
    /// ��������ΪAjax/Remote���󴴽���׼��Ӧ��
    /// </summary>
    [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
        /// <summary>
        /// ����<see cref=��ajaxResponse��/>����
        ///<see cref=��ajaxResponseBase.success��/>is set as true.
        /// </summary>
        public AjaxResponse()
        {

        }

        /// <summary>
        ///����һ��ָ����<see cref=��ajaxResponse��/>�Ķ���
        /// </summary>
        /// <param name="success">ָʾ����ĳɹ�״̬</param>
        public AjaxResponse(bool success)
            : base(success)
        {

        }

        /// <summary>
        ///����һ��ָ����<see cref=��ajaxResponse��/>�Ķ���
        ///<see cref=��ajaxResponseBase.success��/>is set as true.
        /// </summary>
        /// <param name="result">ʵ�ʽ������</param>
        public AjaxResponse(object result)
            : base(result)
        {

        }

        /// <summary>
        ///����һ��ָ����<see cref=��ajaxResponse��/>�Ķ���
        ///<see cref=��ajaxResponseBase.success��/>��Ϊ�١�
        /// </summary>
        /// <param name="error">��������</param>
        /// <param name="unAuthorizedRequest">����ָʾ��ǰ�û�û��ִ�д������Ȩ��</param>
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}