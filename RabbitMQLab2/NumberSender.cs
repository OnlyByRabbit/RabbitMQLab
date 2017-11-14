using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using EasyNetQ;


namespace RabbitMQLab2
{
    class NumberSender : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var message = new Message();
                message.Number = 44;

                bus.Publish(message);
            }
        }
    }
}
