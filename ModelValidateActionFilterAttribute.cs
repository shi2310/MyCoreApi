using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyCoreApi
{
    public class ModelValidateActionFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //公共返回数据类
                MyJsonResult returnMsg = new MyJsonResult();
                string msg = "";
                //获取具体的错误消息
                foreach (var item in context.ModelState.Values)
                {
                    //遍历所有项目的中的所有错误信息
                    foreach (var err in item.Errors)
                    {
                        //消息拼接,用|隔开，前端根据容易解析
                        msg += $"{err.ErrorMessage}|";
                    }
                }
                returnMsg.Msg= msg.Trim('|');
                context.Result = new JsonResult(returnMsg);
            }

        }
    }
}
