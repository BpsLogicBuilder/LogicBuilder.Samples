﻿using CheckMySymptoms.Forms.Parameters.Common;
using LogicBuilder.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace CheckMySymptoms.Forms.Parameters
{
    public class FormDataParameters
    {
        public FormDataParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Form Title")]
            string title,

            [Domain("_500Px,AddressBook,AddressBookOutline,AddressCard,AddressCardOutline,Adjust,Adn,AlignCenter,AlignJustify,AlignLeft,AlignRight,Amazon,Ambulance,AmericanSignLanguageInterpreting,Anchor,Android,Angellist,AngleDoubleDown,AngleDoubleLeft,AngleDoubleRight,AngleDoubleUp,AngleDown,AngleLeft,AngleRight,AngleUp,Apple,Archive,AreaChart,ArrowCircleDown,ArrowCircleLeft,ArrowCircleOutlineDown,ArrowCircleOutlineLeft,ArrowCircleOutlineRight,ArrowCircleOutlineUp,ArrowCircleRight,ArrowCircleUp,ArrowDown,ArrowLeft,ArrowRight,Arrows,ArrowsAlt,ArrowsH,ArrowsV,ArrowUp,AslInterpreting,AssistiveListeningSystems,Asterisk,At,AudioDescription,Automobile,Backward,BalanceScale,Ban,Bandcamp,Bank,BarChart,BarChartOutline,Barcode,Bars,Bath,Bathtub,Battery,Battery0,Battery1,Battery2,Battery3,Battery4,BatteryEmpty,BatteryFull,BatteryHalf,BatteryQuarter,BatteryThreeQuarters,Bed,Beer,Behance,BehanceSquare,Bell,BellOutline,BellSlash,BellSlashOutline,Bicycle,Binoculars,BirthdayCake,Bitbucket,BitbucketSquare,Bitcoin,BlackTie,Blind,Bluetooth,BluetoothB,Bold,Bolt,Bomb,Book,Bookmark,BookmarkOutline,Braille,Briefcase,Btc,Bug,Building,BuildingOutline,Bullhorn,Bullseye,Bus,Buysellads,Cab,Calculator,Calendar,CalendarCheckOutline,CalendarMinusOutline,CalendarOutline,CalendarPlusOutline,CalendarTimesOutline,Camera,CameraRetro,Car,CaretDown,CaretLeft,CaretRight,CaretSquareOutlineDown,CaretSquareOutlineLeft,CaretSquareOutlineRight,CaretSquareOutlineUp,CaretUp,CartArrowDown,CartPlus,Cc,CcAmex,CcDinersClub,CcDiscover,CcJcb,CcMastercard,CcPaypal,CcStripe,CcVisa,Certificate,Chain,ChainBroken,Check,CheckCircle,CheckCircleOutline,CheckSquare,CheckSquareOutline,ChevronCircleDown,ChevronCircleLeft,ChevronCircleRight,ChevronCircleUp,ChevronDown,ChevronLeft,ChevronRight,ChevronUp,Child,Chrome,Circle,CircleOutline,CircleOutlineNotch,CircleThin,Clipboard,ClockOutline,Clone,Close,Cloud,CloudDownload,CloudUpload,Cny,Code,CodeFork,Codepen,Codiepie,Coffee,Cog,Cogs,Columns,Comment,Commenting,CommentingOutline,CommentOutline,Comments,CommentsOutline,Compass,Compress,Connectdevelop,Contao,Copy,Copyright,CreativeCommons,CreditCard,CreditCardAlt,Crop,Crosshairs,Css3,Cube,Cubes,Cut,Cutlery,Dashboard,Dashcube,Database,Deaf,Deafness,Dedent,Delicious,Desktop,Deviantart,Diamond,Digg,Dollar,DotCircleOutline,Download,Dribbble,DriversLicense,DriversLicenseOutline,Dropbox,Drupal,Edge,Edit,Eercast,Eject,EllipsisH,EllipsisV,Empire,Envelope,EnvelopeOpen,EnvelopeOutline,EnvelopeOutlinepenOutline,EnvelopeSquare,Envira,Eraser,Etsy,Eur,Euro,Exchange,Exclamation,ExclamationCircle,ExclamationTriangle,Expand,Expeditedssl,ExternalLink,ExternalLinkSquare,Eye,Eyedropper,EyeSlash,Fa,Facebook,FacebookF,FacebookOfficial,FacebookSquare,FastBackward,FastForward,Fax,Feed,Female,FighterJet,File,FileArchiveOutline,FileAudioOutline,FileCodeOutline,FileExcelOutline,FileImageOutline,FileMovieOutline,FileOutline,FilePdfOutline,FilePhotoOutline,FilePictureOutline,FilePowerpointOutline,FileSoundOutline,FilesOutline,FileText,FileTextOutline,FileVideoOutline,FileWordOutline,FileZipOutline,Film,Filter,Fire,FireExtinguisher,Firefox,FirstOrder,Flag,FlagCheckered,FlagOutline,Flash,Flask,Flickr,FloppyOutline,Folder,FolderOpen,FolderOutline,FolderOutlinepenOutline,Font,FontAwesome,Fonticons,FortAwesome,Forumbee,Forward,Foursquare,FreeCodeCamp,FrownOutline,FutbolOutline,Gamepad,Gavel,Gbp,Ge,Gear,Gears,Genderless,GetPocket,Gg,GgCircle,Gift,Git,Github,GithubAlt,GithubSquare,Gitlab,GitSquare,Gittip,Glass,Glide,GlideG,Globe,Google,GooglePlus,GooglePlusCircle,GooglePlusOfficial,GooglePlusSquare,GoogleWallet,GraduationCap,Gratipay,Grav,Group,HackerNews,HandGrabOutline,HandLizardOutline,HandOutlineDown,HandOutlineLeft,HandOutlineRight,HandOutlineUp,HandPaperOutline,HandPeaceOutline,HandPointerOutline,HandRockOutline,HandScissorsOutline,HandshakeOutline,HandSpockOutline,HandStopOutline,HardOfHearing,Hashtag,HddOutline,Header,Headphones,Heart,Heartbeat,HeartOutline,History,Home,HospitalOutline,Hotel,Hourglass,Hourglass1,Hourglass2,Hourglass3,HourglassEnd,HourglassHalf,HourglassOutline,HourglassStart,Houzz,HSquare,Html5,ICursor,IdBadge,IdCard,IdCardOutline,Ils,Image,Imdb,Inbox,Indent,Industry,Info,InfoCircle,Inr,Instagram,Institution,InternetExplorer,Intersex,Ioxhost,Italic,Joomla,Jpy,Jsfiddle,Key,KeyboardOutline,Krw,Language,Laptop,Lastfm,LastfmSquare,Leaf,Leanpub,Legal,LemonOutline,LevelDown,LevelUp,LifeBouy,LifeBuoy,LifeRing,LifeSaver,LightbulbOutline,LineChart,Link,Linkedin,LinkedinSquare,Linode,Linux,List,ListAlt,ListOl,ListUl,LocationArrow,Lock,LongArrowDown,LongArrowLeft,LongArrowRight,LongArrowUp,LowVision,Magic,Magnet,MailForward,MailReply,MailReplyAll,Male,Map,MapMarker,MapOutline,MapPin,MapSigns,Mars,MarsDouble,MarsStroke,MarsStrokeH,MarsStrokeV,Maxcdn,Meanpath,Medium,Medkit,Meetup,MehOutline,Mercury,Microchip,Microphone,MicrophoneSlash,Minus,MinusCircle,MinusSquare,MinusSquareOutline,Mixcloud,Mobile,MobilePhone,Modx,Money,MoonOutline,MortarBoard,Motorcycle,MousePointer,Music,Navicon,Neuter,NewspaperOutline,None,ObjectGroup,ObjectUngroup,Odnoklassniki,OdnoklassnikiSquare,Opencart,Openid,Opera,OptinMonster,Outdent,Pagelines,PaintBrush,Paperclip,PaperPlane,PaperPlaneOutline,Paragraph,Paste,Pause,PauseCircle,PauseCircleOutline,Paw,Paypal,Pencil,PencilSquare,PencilSquareOutline,Percent,Phone,PhoneSquare,Photo,PictureOutline,PieChart,PiedPiper,PiedPiperAlt,PiedPiperPp,Pinterest,PinterestP,PinterestSquare,Plane,Play,PlayCircle,PlayCircleOutline,Plug,Plus,PlusCircle,PlusSquare,PlusSquareOutline,Podcast,PowerOff,Print,ProductHunt,PuzzlePiece,Qq,Qrcode,Question,QuestionCircle,QuestionCircleOutline,Quora,QuoteLeft,QuoteRight,Ra,Random,Ravelry,Rebel,Recycle,Reddit,RedditAlien,RedditSquare,Refresh,Registered,Remove,Renren,Reorder,Repeat,Reply,ReplyAll,Resistance,Retweet,Rmb,Road,Rocket,RotateLeft,RotateRight,Rouble,Rss,RssSquare,Rub,Ruble,Rupee,S15,Safari,Save,Scissors,Scribd,Search,SearchMinus,SearchPlus,Sellsy,Send,SendOutline,Server,Share,ShareAlt,ShareAltSquare,ShareSquare,ShareSquareOutline,Shekel,Sheqel,Shield,Ship,Shirtsinbulk,ShoppingBag,ShoppingBasket,ShoppingCart,Shower,Signal,SignIn,Signing,SignLanguage,SignOut,Simplybuilt,Sitemap,Skyatlas,Skype,Slack,Sliders,Slideshare,SmileOutline,Snapchat,SnapchatGhost,SnapchatSquare,SnowflakeOutline,SoccerBallOutline,Sort,SortAlphaAsc,SortAlphaDesc,SortAmountAsc,SortAmountDesc,SortAsc,SortDesc,SortDown,SortNumericAsc,SortNumericDesc,SortUp,Soundcloud,SpaceShuttle,Spinner,Spoon,Spotify,Square,SquareOutline,StackExchange,StackOverflow,Star,StarHalf,StarHalfEmpty,StarHalfFull,StarHalfOutline,StarOutline,Steam,SteamSquare,StepBackward,StepForward,Stethoscope,StickyNote,StickyNoteOutline,Stop,StopCircle,StopCircleOutline,StreetView,Strikethrough,Stumbleupon,StumbleuponCircle,Subscript,Subway,Suitcase,SunOutline,Superpowers,Superscript,Support,Table,Tablet,Tachometer,Tag,Tags,Tasks,Taxi,Telegram,Television,TencentWeibo,Terminal,TextHeight,TextWidth,Th,Themeisle,Thermometer,Thermometer0,Thermometer1,Thermometer2,Thermometer3,Thermometer4,ThermometerEmpty,ThermometerFull,ThermometerHalf,ThermometerQuarter,ThermometerThreeQuarters,ThLarge,ThList,ThumbsDown,ThumbsOutlineDown,ThumbsOutlineUp,ThumbsUp,ThumbTack,Ticket,Times,TimesCircle,TimesCircleOutline,TimesRectangle,TimesRectangleOutline,Tint,ToggleDown,ToggleLeft,ToggleOff,ToggleOn,ToggleRight,ToggleUp,Trademark,Train,Transgender,TransgenderAlt,Trash,TrashOutline,Tree,Trello,Tripadvisor,Trophy,Truck,Try,Tty,Tumblr,TumblrSquare,TurkishLira,Tv,Twitch,Twitter,TwitterSquare,Umbrella,Underline,Undo,UniversalAccess,University,Unlink,Unlock,UnlockAlt,Unsorted,Upload,Usb,Usd,User,UserCircle,UserCircleOutline,UserMd,UserOutline,UserPlus,Users,UserSecret,UserTimes,Vcard,VcardOutline,Venus,VenusDouble,VenusMars,Viacoin,Viadeo,ViadeoSquare,VideoCamera,Vimeo,VimeoSquare,Vine,Vk,VolumeControlPhone,VolumeDown,VolumeOff,VolumeUp,Warning,Wechat,Weibo,Weixin,Whatsapp,Wheelchair,WheelchairAlt,Wifi,WikipediaW,WindowClose,WindowCloseOutline,WindowMaximize,WindowMinimize,WindowRestore,Windows,Won,Wordpress,Wpbeginner,Wpexplorer,Wpforms,Wrench,Xing,XingSquare,Yahoo,Yc,YCombinator,YCombinatorSquare,YcSquare,Yelp,Yen,Yoast,Youtube,YoutubePlay,YoutubeSquare")]
            string icon = "Wpforms",

            [Comments("Input validation messages for each field.")]
            List<ValidationMessageParameters> validationMessages = null,

            [Comments("Input validation messages for each field.")]
            List<VariableDirectivesParameters> conditionalDirectives = null
        )
        {
            this.Title = title;
            this.Icon = icon;
            this.ValidationMessages = validationMessages == null
                                        ? new Dictionary<string, Dictionary<string, string>>()
                                        : validationMessages
                                            .Select(vm => new ValidationMessageParameters
                                            {
                                                Field = vm.Field.Replace('.', '_'),
                                                Methods = vm.Methods
                                            })
                                            .ToDictionary(kvp => kvp.Field, kvp => kvp.Methods);
            this.ConditionalDirectives = conditionalDirectives == null
                                            ? new Dictionary<string, List<DirectiveParameters>>()
                                            : conditionalDirectives
                                                .Select(cd => new VariableDirectivesParameters
                                                {
                                                    Field = cd.Field.Replace('.', '_'),
                                                    ConditionalDirectives = cd.ConditionalDirectives
                                                })
                                                .ToDictionary(kvp => kvp.Field, kvp => kvp.ConditionalDirectives);
        }

        public FormDataParameters()
        {
        }

        public string Title { get; set; }
        public string Icon { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
    }

    public class FormDataDummyConstructor
    {
        public FormDataDummyConstructor
        (
            FormDataParameters form,
            RowDataParameters row,
            ColumnDataParameters column,
            Input.InputDataParameters input
        )
        {

        }
    }
}
