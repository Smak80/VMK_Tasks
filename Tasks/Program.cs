// See https://aka.ms/new-console-template for more information
var task = new Task(()=>{Console.WriteLine("Hi!");});
task.Start();
task.Wait();

var ot = Task.Factory.StartNew(() =>
{
    Console.WriteLine("Task 'O' started");
    var it = Task.Factory.StartNew(() =>
    {
        Console.WriteLine("Task 'I' started");
        Thread.Sleep(1000);
        Console.WriteLine("Task 'I' finished");
    }, TaskCreationOptions.AttachedToParent);
    Console.WriteLine("Task 'O' finished");
});
ot.Wait();
var r = new Random();
var tasks = new Task[3]
{
    new Task(() =>
    {
        Thread.Sleep(r.Next(1000));
        Console.WriteLine("Task 1");
    }),
    new Task(() => { 
        Thread.Sleep(r.Next(1000));
        Console.WriteLine("Task 2");
    }),
    new Task(() =>
    {
        Thread.Sleep(r.Next(1000));
        Console.WriteLine("Task 3");
    })
};
foreach (var t in tasks)
{
    t.Start();
}
Task.WaitAny(tasks);

int a = 3;
int b = 5;
var tp = new Task<int>(() => Sum(a, b) );
tp.Start();
var c = tp.Result;
Console.WriteLine(c);
int Sum(int a, int b) => a + b;

var ft = new Task<int>(() =>
{
    Console.WriteLine($"ID={Task.CurrentId}");
    return Sum(a, b);
});
var st = ft.ContinueWith((t) =>
{
    Console.WriteLine($"ID={Task.CurrentId}");
    Console.WriteLine($"Prev ID={t.Id}");
    Console.WriteLine($"Res={t.Result}");
});
ft.Start();
st.Wait();
Console.WriteLine("Main finished");