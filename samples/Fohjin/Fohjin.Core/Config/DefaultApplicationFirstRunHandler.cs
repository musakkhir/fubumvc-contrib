using System;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;

namespace Fohjin.Core.Config
{
    public class DefaultApplicationFirstRunHandler : IApplicationFirstRunHandler
    {
        private readonly IRepository _repository;
        private readonly ISessionSourceConfiguration _sourceConfig;
        private readonly IUnitOfWork _unitOfWork;
        private static bool _isInitialized;

        public DefaultApplicationFirstRunHandler(ISessionSourceConfiguration sourceConfig, IUnitOfWork unitOfWork, IRepository repository)
        {
            _sourceConfig = sourceConfig;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public bool IsInitialized
        {
            get { return _isInitialized; }
            set { _isInitialized = value; }
        }

        public void InitializeIfNecessary()
        {
            if (!_sourceConfig.IsNewDatabase || IsInitialized) return;

            var user = setup_admin_user();
            setup_sample_post(user);
            _unitOfWork.Commit();

            IsInitialized = true;
        }

        private User setup_admin_user()
        {
            var defaultUser = new User
            {
                Username = "Admin",
                Password = "12345",
                DisplayName = "Mark Nijhof",
                Email = "Mark.Nijhof@Gmail.com",
                Url = "http://blog.fohjin.com",
                HashedEmail = "01d418308faffa0d07f34ace68b686ad",
                UserRole = UserRoles.SiteUser,
                TwitterUserName = "MarkNijhof",
            };

            _repository.Save(defaultUser);
            return defaultUser;
        }

        private void setup_sample_post(User user)
        {
            var ndaTag = new Tag {Name = "NDA", CreatedDate = DateTime.Parse("25 NOV 2008")};
            var dipTag = new Tag {Name = "DIP", CreatedDate = DateTime.Parse("25 NOV 2008")};
            var iocTag = new Tag {Name = "IoC", CreatedDate = DateTime.Parse("25 NOV 2008")};
            var dpTag = new Tag {Name = "Design Principles", CreatedDate = DateTime.Parse("25 NOV 2008")};
            var xServiceBusTag = new Tag {Name = "xServiceBus", CreatedDate = DateTime.Parse("25 NOV 2008")};
            var mvcTag = new Tag {Name = "MVC", CreatedDate = DateTime.Parse("08 FEB 2009")};
            var fubuMvcTag = new Tag {Name = "FubuMVC", CreatedDate = DateTime.Parse("08 FEB 2009")};

            var defaultPost2 = new Post
            {
                Title = "Powered by FubuMVC",
                Slug = "Powered_by_FubuMVC",
                Body = "<img src=\"/content/images/FubuMVC_Logo_Medium.jpg\" alt=\"FubuMVC\" style=\"position:relative; float: right; margin: 2px 10px 10px 10px;\">As you can clearly see the design of my blog has changed again, this time it was created by an actual graphical designer <a href=\"http://www.leinan.net/\">Håkon Leinan</a> instead of my own pathetic attempts. But that is not all; the whole blogging engine is replaced with my own implementation using the <a href=\"http://fubumvc.pbwiki.com/\">FubuMVC</a> framework. This is a work in process and I’ll blog about the new additions when they are added.<br /><br /><b>For us, by us</b><br /><br />FubuMVC stands for “For us, by us MVC” and it is a Front Controller-style framework designed primarily for Web applications build upon ASP.NET. It has its origin at <a href=\"http://www.dovetailsoftware.com/\">Dovetail Software</a> where <a href=\"http://codebetter.com/blogs/jeremy.miller\">Jeremy Miller</a>, <a href=\"http://chadmyers.lostechies.com/\">Chad Myers</a> and <a href=\"http://joshuaflanagan.lostechies.com/\">Joshua Flanagan</a> were constantly extending / overriding the ASP.Net MVC beta framework until they arrived at a point where it would make more sense to create a new MCV framework, not based on ASP.Net MVC. This resulted in OpinionatedMVC which was later renamed to FubuMVC. I was lucky to run into this <a href=\"http://code.google.com/p/fubumvc/\">OOS project</a> at an early stage and I have taken the opportunity to learn from these guys with open arms. <br /><br />So you can expect more posts about this new framework where I’ll go more into technical detail and common learning experiences. But before that I have to implement the functionality for adding new blog posts ;)<br /><br /><b>FubuMVC Links</b><br /><br />The Google Code repository: <a href=\"http://code.google.com/p/fubumvc/\">http://code.google.com/p/fubumvc/</a>The wiki page: <a href=\"http://fubumvc.pbwiki.com/\">http://fubumvc.pbwiki.com/</a>The development mailing list: <a href=\"http://groups.google.com/group/fubumvc-devel\">http://groups.google.com/group/fubumvc-devel</a><br /><br /><b>More information</b><br /><br />Jeremy and Chad had a workshop at <a href=\"http://kaizenconf.pbwiki.com/\">Kaizenconf</a> where they explained their Opinionated MVC ideas, <a href=\"http://www.viddler.com/explore/lostechies/videos/2/\">video 1</a> and <a href=\"http://www.viddler.com/explore/lostechies/videos/3/\">video 2</a>. And there is also a blog post from Jeremy <a href=\"http://codebetter.com/blogs/jeremy.miller/archive/2008/10/23/our-opinions-on-the-asp-net-mvc-introducing-the-thunderdome-principle.aspx\">Our \"Opinions\" on the ASP.NET MVC (Introducing the Thunderdome Principle)</a> where he explains the basic idea behind FubuMVC. And then there is also <a href=\"http://herdingcode.com/?p=131\">this</a> recording at <a href=\"http://herdingcode.com/\">Herding Code</a> where Jeremy and Chad go into the actual OSS project FubuMVC. In case you were wondring my name is pronounced Nyhof in English ;)",
                Published = DateTime.Parse("2009-02-08 18:34:00.000"),
                User = user
            };
            defaultPost2.AddTag(fubuMvcTag);
            defaultPost2.AddTag(mvcTag);
            _repository.Save(defaultPost2);

            var defaultPost = new Post
            {
                Title = "The No Dependency Architecture (NDA)",
                Slug = "The_No_Dependency_Architecture_(NDA)",
                Body = "<img src=\"/content/images/nda/nda_128px.jpg\" alt=\"NDA\" style=\"position:relative; float: right; margin: 2px 10px 10px 10px;\">We already have many, many, many acronyms in our profession; TDD, BDD, DDD, SOA, EDA, DIP and SOLID just to name a few.  So who am I to add yet another one in the mix, well I am a nobody. Having covered that, I would like to continue explaining this new one.<br/><br/>NDA is a software architecture that has no dependencies between the different components. It&#8217;s all in the name :)<br/><br/>By combining the Dependency Inversion Principle (DIP) and an Event Driven Architecture (EDA) we can achieve software that has no dependencies, not internal not external. So why would we want that? Well it enables you to just replace any part in your software solution with something else without it affecting the rest of the solution. And you will be able to scale the solution much easier. <br/><br/>So let&#8217;s look a bit more into the details, first I would like to start with DIP. Dependency Injection can be achieved using an Inversion of Control (IoC) container. An IoC container will provide an already instantiated object that (when done properly according to DIP) is based on an interface. The consumer of the object is only using the interface and does not know anything about the actual implementation.<br/><br/>One simple rule about how to provide the dependent objects is that when the consumer object cannot work without the dependent object it should be provided in the constructor of the consumer object; if the consumer object can continue without the dependent object (i.e. logging module) than that object should be provided via a setter property of the consumer object. br/><br/>Modern IoC containers can Auto Wire the dependencies when creating an object, meaning that if a object of the requested type is present in the configuration you will not have to specify the dependent object in the constructor arguments of the consumer, this will be done automatically. I am a fan of keeping the configuration of the IoC container in a configuration file, this way I can easily provide a different implementation of the requested objects by just changing the configuration file. No need to recompile anything. I currently am working a lot with Castle Windsor which is a great IoC container.<br/><br/>So this should take care of dependencies your code may have between other parts of your code (i.e. object between object). Now we can move on to try and get rid of dependencies between different systems or major parts of your system (i.e. between services and consumers).  When using an Event Driven Architecture your code is basically sending out events when something happens and does not care who or what is acting on those events. Different systems may subscribe to these events and may do their own thing. And to take this one step further you should consider using a service bus architecture, which basically means that events will be published into a cue. Consumers of these events subscribe to the cue and will get notified whenever there is an event waiting for them. There can be as many consumers subscribing to the events via the service bus as needed. The publisher does not know nor care about the consumers; it fires the event and moves on. Consumers don&#8217;t know or care about the source of the event, just that it is the type of event they are interested in. <br/><br/>As you can see this decouples the different systems from each other, since none of them know about each other there can be no dependencies between each other. Currently I am looking into NServiceBus, I got a real nice demo today from my college John that started this current thought process.<br/><br/>So now I believe we have a system that does not depend on anything, well realistically it depends on things, but these things can be switched, replaced, multiplied and even be offline nobody knows. I plan to go over these different parts in the coming posts, so if you have any comment I would love to hear from you.<br/><br/>Castle Windsor <a href=\"http://www.castleproject.org/container/index.html\"> http://www.castleproject.org/container/index.html </a><br/>NServiceBus <a href=\"http://www.nservicebus.com/\"> http://www.nservicebus.com/</a><br/>",
                Published = DateTime.Parse("2008-11-25 01:13:00.000"),
                User = user
            };
            defaultPost.AddTag(ndaTag);
            defaultPost.AddTag(dipTag);
            defaultPost.AddTag(iocTag);
            defaultPost.AddTag(dpTag);
            defaultPost.AddTag(xServiceBusTag);

            var otherUser = new User
            {
                Username = "",
                Password = "",
                DisplayName = "Jon Arild Tørresdal",
                Email = "jon@torresdal.net",
                Url = "http://blog.torresdal.net/",
                HashedEmail = "03d8133cd5d1a519234ff01c8cd84dd6",
                UserRole = UserRoles.Visitor

            };
            _repository.Save(otherUser);

            defaultPost.AddComment(
                new Comment
                {
                    Post = defaultPost,
                    User = otherUser,
                    Body = "This is an interesting subject. Would you recommend implementing a system with a complete NDA architecture? As the Agile man I am (at least I think so :-)) this violates some of the principals related to creating only what you need for the task at hand. \"I might want to replace this layer or component some day.\" - is that a good enough reason for the overhead in implementing NDA i full scale? Would it be better to implement it when you need it? What do you think?",
                    Published = DateTime.Parse("27 NOV 2008 22:39:00")
                });
            defaultPost.AddComment(
                new Comment
                {
                    Post = defaultPost,
                    User = user,
                    Body = "Hi Jon,<br/><br/>I started writing you a reply and it became so big that I decided to create a post for it :)<br/><br/>http://blog.fohjin.com/blog/2008/11/29/Is_NDA_in_conflict_with_YAGNI<br/><br/>-Mark",
                    Published = DateTime.Parse("29 NOV 2008 02:10:00")
                });

            _repository.Save(defaultPost);

            var defaultPost1 = new Post
            {
                Title = "Is NDA in conflict with YAGNI?",
                Slug = "Is_NDA_in_conflict_with_YAGNI",
                Body = "<img src=\"/content/images/nda/nda_128px.jpg\" alt=\"NDA\" style=\"position:relative; float: right; margin: 2px 10px 10px 10px;\">I got a question on my previous post (<a href=\"http://blog.fohjin.com/blog/2008/11/25/The_No_Dependency_Architecture_(NDA)\">The No Dependency Architecture (NDA)</a>) if NDA is not in conflict with YAGNI, and when I was writing the response it became more or less an actual post, so there you go :)<br/><br/>Explaining the thought behind the No Dependency Architecture (NDA) in combination with YAGNI in one sentence would be something like this: <b>&#8220;You anticipate change at a technical level, not at a functional level&#8221;</b>.<br/><br/>I believe that the &#8220;You Ain&#8217;t Gonna Need It&#8221; (YAGNI) principle does not imply that writing software accordingly to the SOLID principles is bad practice. When you are following these principles (and there are others that are good to follow too) then you are writing software that assumes change, right? I believe that YAGNI is more targeted to functional overhead.<br/><br/>I.e.  using a IoC container to fulfill the Dependency Inversion Principle (DIP) is not in conflict with YAGNI because that is at a technical level, but when you are writing extra code for no other reason than that you assume the object might be used in a certain other way also, then you are violating the YAGNI principle because then you are creating additional functionality that is not needed.<br/><br/>From Wikipedia (<a href=\"http://en.wikipedia.org/wiki/You_Ain't_Gonna_Need_It\">http://en.wikipedia.org/wiki/You_Ain't_Gonna_Need_It</a>) YAGNI, short for 'You Ain't Gonna Need It', suggests to programmers that they should not add <b>functionality</b> until it is necessary<br/><br/><b>So I would recommend using the various SOLID principles when writing your software assuming things will change because that is just good practice. Change is a fact, we just don&#8217;t know what will change, but by conforming to SOLID we can handle these future changes without too much headache. </b> <br/><br/>As to using an Event Driven Architecture, I am surely not saying each object should only interact using events, that would not make much sense, but most (larger) systems will be build up using multiple sub systems. Having these sub systems communicate via events is a very good way to isolate them from each other. Then it is unknown to each sub system who they are really talking to (i.e. dispatching the events to). In fact it is a 1 to n relationship where the dispatched event might have no, one or many subscribers.<br/><br/>To make the actual handling of subscribing and publishing easier you could use a service bus that deal with that for you. This will also make the system easier adaptable to change. You can also specify that the event can be handled by any instance of the same subscriber enabling load balancing.<br/><br/>&#8220;Don&#8217;t call us, we call you&#8221; (<a href=\"http://en.wikipedia.org/wiki/Hollywood_Principle\">http://en.wikipedia.org/wiki/Hollywood_Principle</a>); is the basis of an Event Driven Architecture, and this is very true, because no system is waiting on an answer from another system. They fire events and they act on events, it is merely coincidence if these events have anything to do with each other.<br/><br/><b>So I would also recommend using an Event Driven Architecture (EDA) straight from the beginning, perhaps not using a service bus from the start, but just using EDA will make refactoring to use one at a later stage a lot easier.</b>",
                Published = DateTime.Parse("2008-11-29 01:53:00.000"),
                User = user
            };
            defaultPost1.AddTag(ndaTag);
            defaultPost1.AddTag(dipTag);
            defaultPost1.AddTag(iocTag);
            defaultPost1.AddTag(dpTag);
            defaultPost1.AddTag(xServiceBusTag);

            _repository.Save(defaultPost1);
        }
    }
}