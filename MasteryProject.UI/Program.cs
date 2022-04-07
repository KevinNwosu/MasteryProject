using MasteryProject.UI;
using Ninject;

NinjectContainer.Configure();
Controller controller = NinjectContainer.kernel.Get<Controller>();
controller.Run();